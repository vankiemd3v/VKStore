
using VKStore.Data.EF;
using VKStore.Data.Entities;
using VKStore.Utilities.Exceptions;
using VKStore.ViewModels.Catalog.Products;
using VKStore.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using VKStore.Application.Common;
using Azure.Core;
using VKStore.ViewModels.Catalog.ProductImages;
using VKStore.ViewModels.Catalog.Categories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace VKStore.Application.Catalog.Products
{
    public class ProductService : IProductService
    { 
        private readonly VKStoreDbContext _context;
        private readonly IStorageService _storageService;
        public ProductService(VKStoreDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        public async Task<int> AddImage(int productId, ProductImageCreateRequest request)
        {
            var productImage = new ProductImage()
            {
                IsDefault = request.IsDefault,
                ProductId = productId,
            };

            if (request.ImageFile != null)
            {
                productImage.ImagePath = await this.SaveFile(request.ImageFile);
                productImage.FileSize = request.ImageFile.Length;
            }
            _context.ProductImages.Add(productImage);
            await _context.SaveChangesAsync();
            return productImage.Id;
        }

        public async Task<int> Create(ProductCreateRequest request)
        {
            var product = new Product()
            {
                System= request.System,
                Description = request.Description,
                Quantity = request.Quantity,
                Name = request.Name,
                Price = request.Price,
                CategoryId = request.CategoryId,
                CreatedDate = DateTime.Now,
            };
            // Chọn ảnh
            if (request.ThumbnailImage != null)
            {
                product.ProductImages = new List<ProductImage>()
                {
                    new ProductImage()
                    {
                        IsDefault= true,
                        FileSize = request.ThumbnailImage.Length,
                        ImagePath = await this.SaveFile(request.ThumbnailImage),
                    }
                };
            }
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product.Id;
        }

        public async Task<int> Delete(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new VKStoreException($"Không tìm thấy sản phẩm: {productId}");
            var images = _context.ProductImages.Where(x => x.ProductId == productId);
            foreach ( var image in images)
            {
                await _storageService.DeleteFileAsync(image.ImagePath);
            }
            _context.Products.Remove(product);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteImage(int imageId)
        {
            var productImage = await _context.ProductImages.FindAsync(imageId);
            if (productImage == null)
                throw new VKStoreException($"hông tìm thấy hình ảnh với id: {imageId}");
            _context.ProductImages.Remove(productImage);
            return await _context.SaveChangesAsync();
        }

        public async Task<PagedResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request)
        {
            // 1. join 2 bảng
            var query = from p in _context.Products
                        join c in _context.Categories on p.Category.Id equals c.Id
                        select new { p, c };
            // 2. lọc
            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x=>x.p.Name.Contains(request.Keyword));
            if (request.CategoryId != null && request.CategoryId > 0)
                query = query.Where(x => x.c.Id == request.CategoryId);
            // 3. Bảng
            int totalRow = await query.CountAsync();
            // skip = dừng ở bảng ghi
            // take = lấy bảng ghi
            // ví dụ: pageIndex = 1, pageSize = 10, Skip 1-1=0*10=0    => lấy 10 bảng ghi đầu tiên
            //        pageIndex = 2, pageSize = 10, Skip 2-1=1*10=10   => lấy 10 bảng ghi thứ 2
            var data = await query.OrderByDescending(x=>x.p.CreatedDate).Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
                            .Select(x=> new ProductViewModel()
                            {
                                Id = x.p.Id,
                                Name = x.p.Name,
                                Price = x.p.Price,
                                Quantity = x.p.Quantity,
                                CategoryName = x.c.Name
                            }).ToListAsync();

            // 4. Trả về kết quả
            var pagedResult = new PagedResult<ProductViewModel>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            return pagedResult;
        }

        public async Task<ProductViewModel> GetById(int productId)
        {
            // 1. join 2 bảng
            var query = from p in _context.Products
                        join c in _context.Categories on p.Category.Id equals c.Id
                        join pi in _context.ProductImages on p.Id equals pi.ProductId
                        where p.Id == productId
                        select new { p, c, pi };
            var product = await query.Select(x => new ProductViewModel()
            {
                Id = x.p.Id,
                System = x.p.System,
                Name = x.p.Name,
                Price = x.p.Price,
                Quantity = x.p.Quantity,
                CategoryName = x.c.Name,
                ImagePath = x.pi.ImagePath,
                Description = x.p.Description,
                CategoryId = x.c.Id,
                ThumbnailImage = x.pi.ImagePath

            }).FirstOrDefaultAsync();
            return product;
        }

        public async Task<ProductImageViewModel> GetImageById(int imageId)
        {
            var image = await _context.ProductImages.FindAsync(imageId);
            if (image == null)
                throw new VKStoreException($"Không tìm thấy hình ảnh với id: {imageId}");

            var viewModel = new ProductImageViewModel()
            {
                FileSize = image.FileSize,
                Id = image.Id,
                ImagePath = image.ImagePath,
                IsDefault = image.IsDefault,
                ProductId = image.ProductId
            };
            return viewModel;
        }

        public async Task<List<ProductImageViewModel>> GetListImage(int productId)
        {
            return await _context.ProductImages.Where(x => x.ProductId == productId)
                .Select(i => new ProductImageViewModel()
                {
                    FileSize = i.FileSize,
                    Id = i.Id,
                    ImagePath = i.ImagePath,
                    IsDefault = i.IsDefault,
                    ProductId = i.ProductId
                }).ToListAsync();
        }
        public async Task<List<CategoryViewModel>> GetListCategory()
        {
            return await _context.Categories.Where(x=>x.ParentId != null)
                .Select(i => new CategoryViewModel()
                {
                    Id = i.Id,
                    Name= i.Name,
                    ParentId= i.ParentId,
                    Status= i.Status
                }).ToListAsync();
        }

        public async Task<int> Update(ProductUpdateRequest request)
        {
            var product = await _context.Products.FindAsync(request.Id);
            if (product == null) throw new VKStoreException($"Không tìm thấy sản phẩm: {request.Id}");
            product.System = request.System;
            product.Name = request.Name;
            product.Price = request.Price;
            product.CategoryId = request.CategoryId;
            product.Quantity = request.Quantity;
            product.Description = request.Description;

            // Chọn ảnh
            if (request.ThumbnailImage != null)
            {
                var thumbnailImage = await _context.ProductImages.FirstOrDefaultAsync(x=>x.ProductId == request.Id && x.IsDefault == true);
                if(thumbnailImage != null)
                {
                    thumbnailImage.FileSize = request.ThumbnailImage.Length;
                    thumbnailImage.ImagePath = await this.SaveFile(request.ThumbnailImage);
                    _context.ProductImages.Update(thumbnailImage);
                }
            }
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateImage(int imageId, ProductImageUpdateRequest request)
        {
            var productImage = await _context.ProductImages.FindAsync(imageId);
            if (productImage == null)
                throw new VKStoreException($"Không tìm thấy hình ảnh với id: {imageId}");

            if (request.ImageFile != null)
            {
                productImage.ImagePath = await this.SaveFile(request.ImageFile);
                productImage.FileSize = request.ImageFile.Length;
            }
            _context.ProductImages.Update(productImage);
            return await _context.SaveChangesAsync();
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
        public async Task<PagedResult<ProductViewModel>> GetAllByCategoryId(GetPublicProductPagingRequest request)
        {
            request.PageIndex = 1;
            request.PageSize = 2;
            // 1. join 2 bảng
            var query = from p in _context.Products
                        join c in _context.Categories on p.Category.Id equals c.Id
                        where p.CategoryId == request.CategoryId
                        select new { p, c };
            // 3. Bảng
            int totalRow = await query.CountAsync();
            // skip = dừng ở bảng ghi
            // take = lấy bảng ghi
            // ví dụ: pageIndex = 1, pageSize = 10, Skip 1-1=0*10=0    => lấy 10 bảng ghi đầu tiên
            //        pageIndex = 2, pageSize = 10, Skip 2-1=1*10=10   => lấy 10 bảng ghi thứ 2
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
                            .Select(x => new ProductViewModel()
                            {
                                Name = x.p.Name,
                                Price = x.p.Price,
                                CategoryName = x.c.Name
                            }).ToListAsync();

            // 4. Trả về kết quả
            var pagedResult = new PagedResult<ProductViewModel>()
            {
                TotalRecords = totalRow,
                PageIndex= request.PageIndex,
                PageSize    = request.PageSize,
                Items = data
            };
            return pagedResult;
        }

        public async Task<List<ProductViewModel>> GetListProduct(int take, int? categoryId, string? keyword)
        {
            var query = from i in _context.ProductImages
                        join p in _context.Products on i.Product.Id equals p.Id
                        join c in _context.Categories on p.CategoryId equals c.Id
                        select new { p, i , c};
            if (categoryId == null && keyword == null)
            {
               var products = await query.OrderByDescending(x => x.p.CreatedDate).Take(take)
                            .Select(x => new ProductViewModel()
                            {
                                Id= x.p.Id,
                                Name = x.p.Name,
                                Price = x.p.Price,
                                ImagePath = x.i.ImagePath
                            }).ToListAsync();
                return products;
            }
            else if (categoryId == null && keyword != null)
            {
                var products = await query.Where(x => x.p.Name.Contains(keyword)).OrderByDescending(x => x.p.CreatedDate).Take(take)
                            .Select(x => new ProductViewModel()
                            {
                                Id= x.p.Id,
                                Name = x.p.Name,
                                Price = x.p.Price,
                                ImagePath = x.i.ImagePath
                            }).ToListAsync();
                return products;
            }
            else 
            {
                var category = await _context.Categories.Where(x => x.Id == categoryId).SingleOrDefaultAsync();
                if(category.ParentId != null)
                {
                    var products = await query.Where(x => x.p.CategoryId == categoryId).OrderByDescending(x => x.p.CreatedDate).Take(take)
                            .Select(x => new ProductViewModel()
                            {
                                Id = x.p.Id,
                                Name = x.p.Name,
                                Price = x.p.Price,
                                ImagePath = x.i.ImagePath,
                                CategoryName = x.c.Name,
                                CategoryId = x.c.Id
                            }).ToListAsync();
                    return products;
                }
                else
                {
                    var result = new List<ProductViewModel>();
                    var childrensCategory = await _context.Categories.Where(x=>x.ParentId == categoryId).ToListAsync();
                    
                    foreach (var item in childrensCategory)
                    {
                        var products = await query.Where(x => x.p.CategoryId == item.Id).ToListAsync();
                        foreach (var product in products)
                        {
                            var categoryParentName = await _context.Categories.Where(x => x.Id == categoryId).Select(x=>x.Name).SingleOrDefaultAsync();
                            result.Add(new ProductViewModel()
                            {
                                Id = product.p.Id,
                                Name = product.p.Name,
                                Price = product.p.Price,
                                ImagePath = product.i.ImagePath,
                                CategoryParentName = categoryParentName,
                                CategoryId = product.c.Id
                            });
                        }
                        
                    }
                    return result;
                }
            }
            
        }

        public async Task<List<ProductViewModel>> GetProductByCategoryId(int categoryId)
        {
            var query = from i in _context.ProductImages
                        join p in _context.Products on i.Product.Id equals p.Id
                        select new { p, i };
            var products = await query.OrderByDescending(x => x.p.CreatedDate)
                            .Select(x => new ProductViewModel()
                            {
                                Name = x.p.Name,
                                Price = x.p.Price,
                                ImagePath = x.i.ImagePath
                            }).ToListAsync();
            return products;
        }
    }
}

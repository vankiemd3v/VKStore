
using VKStore.ViewModels.Catalog.Products;
using VKStore.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using VKStore.ViewModels.Catalog.ProductImages;
using VKStore.ViewModels.Catalog.Categories;

namespace VKStore.Application.Catalog.Products
{
    public interface IProductService
    {
        Task<int> Create(ProductCreateRequest request);
        Task<int> Update(ProductUpdateRequest request);
        Task<int> Delete(int productId);
        Task<ProductViewModel> GetById(int productId);
        Task<PagedResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request);

        // ảnh
        Task<List<ProductImageViewModel>> GetListImage(int productId);
        Task<List<CategoryViewModel>> GetListCategory();
        Task<int> AddImage(int productId, ProductImageCreateRequest request);
        Task<int> UpdateImage(int imageId, ProductImageUpdateRequest request);
        Task<int> DeleteImage(int imageId);
        Task<ProductImageViewModel> GetImageById(int imageId);
        Task<PagedResult<ProductViewModel>> GetAllByCategoryId(GetPublicProductPagingRequest request);
        Task<List<ProductViewModel>> GetProductByCategoryId(int categoryId);
        Task<List<ProductViewModel>> GetListProduct(int take, int? categoryId, string? keyword);
    }
}

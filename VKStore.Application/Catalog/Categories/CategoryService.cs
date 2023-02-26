
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
using VKStore.ViewModels.Catalog.Slide;

namespace VKStore.Application.Catalog.Products
{
    public class CategoryService : ICategoryService
    {
        private readonly VKStoreDbContext _context;
        private readonly IStorageService _storageService;
        public CategoryService(VKStoreDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        public async Task<List<CategoryViewModel>> GetAll()
        {
            var categories = await _context.Categories
                .Select(x => new CategoryViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    ParentId = x.ParentId,
                    Status = x.Status
                }).ToListAsync();
            return categories;
        }

        public async Task<List<CategoryViewModel>> GetCategoriesParent()
        {
            
            var parents = await _context.Categories.Where(x => x.ParentId == null).ToListAsync();
            var result = new List<CategoryViewModel>();
            
            foreach (var parent in parents)
            {
                int totalParent = 0; int totalChildren = 0;
                var childrens = await _context.Categories.Where(x => x.ParentId == parent.Id).ToListAsync();
                foreach (var item in childrens)
                {
                    var numberProduct = await _context.Products.Where(x => x.CategoryId == item.Id).CountAsync();
                    totalChildren = totalChildren + numberProduct;
                }
                totalParent = totalParent + totalChildren;
                result.Add(new CategoryViewModel()
                {
                    Id = parent.Id,
                    Name = parent.Name,
                    ParentId = parent.ParentId,
                    Status = parent.Status,
                    TotalProduct = totalParent
                });
            }
            return result;
        }
    }
}

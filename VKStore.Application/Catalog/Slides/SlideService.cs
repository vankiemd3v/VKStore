
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
    public class SlideService : ISlideService
    {
        private readonly VKStoreDbContext _context;
        private readonly IStorageService _storageService;
        public SlideService(VKStoreDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        public async Task<List<SlideViewModel>> GetAll()
        {
            var slides = await _context.Slides.OrderBy(x=>x.SortOrder)
                .Select(x=> new SlideViewModel()
            {
                Id = x.Id,
                Name= x.Name,
                Description= x.Description,
                Image = x.Image,
                Url= x.Url,
                SortOrder   = x.SortOrder,
                Status  = x.Status
            }).ToListAsync();
            return slides;
        }
    }
}

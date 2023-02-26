
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
using VKStore.ViewModels.Catalog.Slide;

namespace VKStore.Application.Catalog.Products
{
    public interface ISlideService
    {
        Task<List<SlideViewModel>> GetAll(); 
    }
}

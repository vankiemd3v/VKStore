using Microsoft.AspNetCore.Mvc;
using VKStore.ApiIntergration;
using VKStore.Application.Catalog.Products;
using VKStore.ViewModels.Common;

namespace VKStore.WebApp.Controllers.Components
{
    public class SideBarViewComponent: ViewComponent
    {
        private readonly ICategoryApiClient _categoryApiClient;
        public SideBarViewComponent(ICategoryApiClient categoryApiClient)
        {
            _categoryApiClient = categoryApiClient;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _categoryApiClient.GetAll();
            if(categories != null)
            {
                return View(categories);
            }
            return View(null);
        }
    }
}

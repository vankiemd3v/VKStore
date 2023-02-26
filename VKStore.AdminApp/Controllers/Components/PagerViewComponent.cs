using Microsoft.AspNetCore.Mvc;
using VKStore.ViewModels.Common;
using static VKStore.ViewModels.Common.PagedResultBase;

namespace VKStore.AdminApp.Controllers.Components
{
    public class PagerViewComponent:ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(PagedResultBase result)
        {
            return Task.FromResult((IViewComponentResult)View("Default", result));
        }
    }
}

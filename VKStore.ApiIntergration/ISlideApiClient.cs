using VKStore.ViewModels.Catalog.Categories;
using VKStore.ViewModels.Catalog.Products;
using VKStore.ViewModels.Catalog.Slide;
using VKStore.ViewModels.Common;
using VKStore.ViewModels.System.Users;

namespace VKStore.ApiIntergration
{
    public interface ISlideApiClient
    {
        Task<List<SlideViewModel>> GetAll();
    }
}

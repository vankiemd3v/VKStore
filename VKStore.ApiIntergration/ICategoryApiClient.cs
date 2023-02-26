using VKStore.ViewModels.Catalog.Categories;
using VKStore.ViewModels.Common;
using VKStore.ViewModels.System.Roles;

namespace VKStore.ApiIntergration
{
    public interface ICategoryApiClient 
    {
        Task<List<CategoryViewModel>> GetAll();
        Task<List<CategoryViewModel>> GetCategoriesParent();
    }
}

using VKStore.ViewModels.Catalog.Categories;
using VKStore.ViewModels.Catalog.Products;
using VKStore.ViewModels.Common;
using VKStore.ViewModels.System.Users;

namespace VKStore.ApiIntergration
{
    public interface IProductApiClient
    {
        Task<PagedResult<ProductViewModel>> GetProductsPagings(GetManageProductPagingRequest request);
        Task<bool> CreateProduct(ProductCreateRequest request);
        Task<bool> UpdateProduct(ProductUpdateRequest request);
        Task<ApiResult<List<CategoryViewModel>>> GetListCategory();
        Task<List<ProductViewModel>> GetListProduct(int take, int? categoryId);
        //Task<ApiResult<List<ProductViewModel>>> GetListProductByCategory(int categoryId);
        string GetUrlApi();
        Task<ProductViewModel> GetProduct(int id);
        Task<ProductViewModel> GetPublicProductById(int id);
    }
}

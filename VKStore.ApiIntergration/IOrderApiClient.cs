using VKStore.ViewModels.Catalog.Orders;
using VKStore.ViewModels.Catalog.Products;
using VKStore.ViewModels.Common;
using VKStore.ViewModels.System.Roles;

namespace VKStore.ApiIntergration
{
    public interface IOrderApiClient
    {
        Task<PagedResult<OrderViewModel>> GetOrdersPagings(GetOrdersPagingRequest request);
        Task<bool> CreateOrder(CreateOrderRequest request);
        Task<OrderViewModel> Detail(int id);
        Task<bool> UpdateStatusOrder(UpdateStatusOrderRequest request);
    }
}

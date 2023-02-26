
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
using VKStore.ViewModels.Catalog.Orders;

namespace VKStore.Application.Catalog.Orders
{
    public interface IOrderService
    {
        Task<PagedResult<OrderViewModel>> GetAllPaging(GetOrdersPagingRequest request);
        Task<bool> CreateOrder(CreateOrderRequest request);
        Task<OrderViewModel> Detail(int id);
        Task<bool> UpdateStatusOrder(UpdateStatusOrderRequest request);
    }
}

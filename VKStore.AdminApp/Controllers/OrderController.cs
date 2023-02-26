using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing.Printing;
using VKStore.ApiIntergration;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using VKStore.Data.Entities;
using VKStore.ViewModels.Catalog.Products;
using VKStore.ViewModels.Catalog.Orders;
using VKStore.Data.Enums;

namespace VKStore.AdminApp.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderApiClient _orderApiClient;
        public OrderController(IOrderApiClient orderApiClient)
        {
            _orderApiClient= orderApiClient;
        }
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var session = HttpContext.Session.GetString("Token");
            var request = new GetOrdersPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            ViewBag.Keyword = keyword;
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            var data = await _orderApiClient.GetOrdersPagings(request);
            return View(data);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var detail = await _orderApiClient.Detail(id);
            ViewBag.Product = detail.OrderDetails;
            return View(detail);
        }
        [HttpPost]
        public async Task<IActionResult> Detail(int id, string status)
        {
            UpdateStatusOrderRequest request = new UpdateStatusOrderRequest()
            {
                Id = id,
                Status = status
            };
            var update = await _orderApiClient.UpdateStatusOrder(request);
            if (update)
            {
                return RedirectToAction("Index", "Order");
            }
            return View(request);
        }
    }
}

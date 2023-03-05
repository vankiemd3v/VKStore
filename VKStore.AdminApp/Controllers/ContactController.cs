using Microsoft.AspNetCore.Mvc;
using VKStore.ApiIntergration;
using VKStore.ViewModels.Catalog.Contacts;
using VKStore.ViewModels.Catalog.Orders;

namespace VKStore.AdminApp.Controllers
{
    public class ContactController : BaseController
    {
        private readonly IContactApiClient _contactApiClient;
        public ContactController(IContactApiClient contactApiClient)
        {
            _contactApiClient = contactApiClient;
        }
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var session = HttpContext.Session.GetString("Token");
            var request = new GetContactPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            ViewBag.Keyword = keyword;
            var data = await _contactApiClient.GetContactPagings(request);
            return View(data);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var detail = await _contactApiClient.Detail(id);
            return View(detail);
        }
    }
}

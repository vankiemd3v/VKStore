using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using VKStore.ApiIntergration;
using VKStore.Data.Entities;
using VKStore.ViewModels.Catalog.Categories;
using VKStore.ViewModels.Catalog.Products;
using VKStore.ViewModels.Common;
using VKStore.ViewModels.System.Users;

namespace VKStore.AdminApp.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductApiClient _productApiClient;
        private readonly IConfiguration _configuration;
        public ProductController(IProductApiClient productApiClient, IConfiguration configuration)
        {
            _productApiClient = productApiClient;
            _configuration = configuration;
        }
        public async Task<IActionResult> Index(int categoryId, string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var session = HttpContext.Session.GetString("Token");
            var request = new GetManageProductPagingRequest()
            {
                CategoryId = categoryId,
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            ViewBag.Keyword = keyword;
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            var categories = await _productApiClient.GetListCategory();
            ViewBag.ListCategory = categories.ResultObj.Select(x=> new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text= x.Name,
                Selected = categoryId == x.Id
            });
            //List<SelectListItem> items = new();
            //foreach (var item in categories.ResultObj)
            //{
            //    var category = new SelectListItem { Value = item.Id.ToString(), Text = item.Name, Selected = };
            //    items.Add(category);
            //};
            //ViewBag.ListCategory = items;
            var data = await _productApiClient.GetProductsPagings(request);
            return View(data);
        }
        public async Task<IActionResult> Create()
        {
            var categories = await _productApiClient.GetListCategory();
            List<SelectListItem> items = new();
            foreach (var item in categories.ResultObj)
            {
                var category = new SelectListItem { Value = item.Id.ToString(), Text = item.Name };
                items.Add(category);
            };
            ViewBag.ListCategory = items;
            return View();
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
        {

            // bắt lỗi validation
            if (!ModelState.IsValid)
                return View();
            var result = await _productApiClient.CreateProduct(request);
            if (result)
            {
                TempData["result"] = "Thêm sản phẩm thành công";
                return RedirectToAction("Index", "Product");
            }
            // fail trả về 1 error message
            ModelState.AddModelError("", "Thêm sản phẩm thất bại");
            return View(request);
        }
        public async Task<IActionResult> Update(int id)
        {
            var categories = await _productApiClient.GetListCategory();
            var product = await _productApiClient.GetProduct(id);
            List<SelectListItem> items = new();
            foreach (var item in categories.ResultObj)
            {
                var category = new SelectListItem { Value = item.Id.ToString(), Text = item.Name };
                items.Add(category);
            };
            ViewBag.ListCategory = items;
            return View(product);
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update( ProductUpdateRequest request)
        {

            // bắt lỗi validation
             if (!ModelState.IsValid)
                return View(ModelState);
            var result = await _productApiClient.UpdateProduct(request);
            if (result)
            {
                TempData["result"] = "Cập nhật sản phẩm thành công";
                return RedirectToAction("Index", "Product");
            }
            // fail trả về 1 error message
            ModelState.AddModelError("", "Cập nhật sản phẩm thất bại");
            return View(request);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VKStore.ApiIntergration;
using VKStore.Data.Entities;
using VKStore.ViewModels.Catalog.Contacts;
using VKStore.WebApp.Models;

namespace VKStore.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISlideApiClient _slideApiClient;
        private readonly IProductApiClient _productApiClient;
        private readonly ICategoryApiClient _categoryApiClient;
        private readonly IContactApiClient _contactApiClient;
        public HomeController(ILogger<HomeController> logger, ISlideApiClient slideApiClient, IProductApiClient productApiClient, ICategoryApiClient categoryApiClient, IContactApiClient contactApiClient)
        {
            _logger = logger;
            _slideApiClient = slideApiClient;
            _productApiClient = productApiClient;
            _categoryApiClient = categoryApiClient;
            _contactApiClient = contactApiClient;
        }

        public async Task<IActionResult> Index()
        {
            var slideViewModel = new HomeViewModel()
            {
                Slides = await _slideApiClient.GetAll(),
                Products = await _productApiClient.GetListProduct(12, null, null),
                Categories = await _categoryApiClient.GetCategoriesParent()
            };
            ViewBag.GetUrlApi = GetUrlImage();
            return View(slideViewModel);
        }
        public async Task<IActionResult> Category(int categoryId)
        {
            var homeViewModel = new HomeViewModel()
            {
                Products = await _productApiClient.GetListProduct(100, categoryId, null),
                Categories = await _categoryApiClient.GetCategoriesParent()
            };
            if (categoryId != null)
            {

                ViewBag.CategoryName = homeViewModel.Products.Select(x => x.CategoryParentName).FirstOrDefault();
            }
            ViewBag.GetUrlApi = GetUrlImage();
            return View(homeViewModel);
        }
        public async Task<IActionResult> Product(string keyword)
        {
            var homeViewModel = new HomeViewModel()
            {
                Products = await _productApiClient.GetListProduct(100, null, keyword),
                Categories = await _categoryApiClient.GetCategoriesParent()
            };
            ViewBag.Keyword = keyword;
            ViewBag.GetUrlApi = GetUrlImage();
            return View(homeViewModel);
        }
        public async Task<IActionResult> Details(int id)
        {
            var homeViewModel = new HomeViewModel();
            homeViewModel.Product = await _productApiClient.GetPublicProductById(id);
            var categoryId = homeViewModel.Product.CategoryId;
            homeViewModel.Products = await _productApiClient.GetListProduct(100, categoryId, null);
            ViewBag.GetUrlApi = GetUrlImage();
            return View(homeViewModel);
        }
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Contact(CreateContactRequest request)
        {
            var contact = await _contactApiClient.CreateContact(request);
            return RedirectToAction("ContactSuccess","Home");
        }
        public IActionResult ContactSuccess()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private string GetUrlImage()
        {
            var url = _productApiClient.GetUrlApi();
            var UrlImage = url + "user-content";
            return UrlImage;
        }
    }
}
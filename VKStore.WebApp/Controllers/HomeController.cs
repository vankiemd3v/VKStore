using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VKStore.ApiIntergration;
using VKStore.Data.Entities;
using VKStore.WebApp.Models;

namespace VKStore.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISlideApiClient _slideApiClient;
        private readonly IProductApiClient _productApiClient;
        private readonly ICategoryApiClient _categoryApiClient;
        public HomeController(ILogger<HomeController> logger, ISlideApiClient slideApiClient, IProductApiClient productApiClient, ICategoryApiClient categoryApiClient)
        {
            _logger = logger;
            _slideApiClient = slideApiClient;
            _productApiClient = productApiClient;
            _categoryApiClient = categoryApiClient;
        }

        public async Task<IActionResult> Index()
        {
            var slideViewModel = new HomeViewModel()
            {
                Slides = await _slideApiClient.GetAll(),
                Products = await _productApiClient.GetListProduct(12, null),
                Categories = await _categoryApiClient.GetCategoriesParent()
            };
            var url = _productApiClient.GetUrlApi();
            var UrlImage = url + "user-content";
            ViewBag.GetUrlApi = UrlImage;
            return View(slideViewModel);
        }
        public async Task<IActionResult> Category(int categoryId)
        {
            var homeViewModel = new HomeViewModel()
            {
                Products = await _productApiClient.GetListProduct(12, categoryId),
                Categories = await _categoryApiClient.GetCategoriesParent()
            };
            if (categoryId != null)
            {

                ViewBag.CategoryName = homeViewModel.Products.Select(x => x.CategoryParentName).FirstOrDefault();
            }
            var url = _productApiClient.GetUrlApi();
            var UrlImage = url + "user-content";
            ViewBag.GetUrlApi = UrlImage;
            return View(homeViewModel);
        }
        public async Task<IActionResult> Product()
        {
            var homeViewModel = new HomeViewModel()
            {
                Products = await _productApiClient.GetListProduct(100, null),
                Categories = await _categoryApiClient.GetCategoriesParent()
            };
            var url = _productApiClient.GetUrlApi();
            var UrlImage = url + "user-content";
            ViewBag.GetUrlApi = UrlImage;
            return View(homeViewModel);
        }
        public async Task<IActionResult> Details(int id)
        {
            var homeViewModel = new HomeViewModel();
            homeViewModel.Product = await _productApiClient.GetPublicProductById(id);
            var categoryId = homeViewModel.Product.CategoryId;
            homeViewModel.Products = await _productApiClient.GetListProduct(100, categoryId);
            var url = _productApiClient.GetUrlApi();
            var UrlImage = url + "user-content";
            ViewBag.GetUrlApi = UrlImage;
            return View(homeViewModel);
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
    }
}
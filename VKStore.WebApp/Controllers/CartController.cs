using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Newtonsoft.Json;
using System.Security.Policy;
using VKStore.ApiIntergration;
using VKStore.Data.Enums;
using VKStore.Ultilities.Constants;
using VKStore.ViewModels.Catalog.Orders;
using VKStore.WebApp.Models;

namespace VKStore.WebApp.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductApiClient _productApiClient;
        private readonly IOrderApiClient _orderApiClient;
        public CartController(IProductApiClient productApiClient, IOrderApiClient orderApiClient)
        {
            _productApiClient = productApiClient;
            _orderApiClient = orderApiClient;
        }
        public IActionResult Index()
        {
            ViewBag.GetUrlApi = GetUrlImage();
            return View();
        }
        public async Task<IActionResult> AddToCart(int id)
        {
            int quantity;
            var product = await _productApiClient.GetPublicProductById(id);
            var session = HttpContext.Session.GetString(SystemConstants.CartSession);
            List<CartItemViewModel> currentCart = new List<CartItemViewModel>();
            if (session != null)
                currentCart = JsonConvert.DeserializeObject<List<CartItemViewModel>>(session);
            if (currentCart.Any(x => x.ProductId == id))
            {
                var item = currentCart.Where(x => x.ProductId == id).SingleOrDefault();
                item.Quantity += 1;
            }
            else
            {
                var cartItem = new CartItemViewModel()
                {
                    ProductId = id,
                    Image = product.ThumbnailImage,
                    Name = product.Name,
                    Quantity = 1,
                    Price = product.Price,
                };
                currentCart.Add(cartItem);
            }

            HttpContext.Session.SetString(SystemConstants.CartSession, JsonConvert.SerializeObject(currentCart));
            return Ok(currentCart);
        }
        public IActionResult UpdateCart(int id, int quantity)
        {
            var session = HttpContext.Session.GetString(SystemConstants.CartSession);
            List<CartItemViewModel> currentCart = new List<CartItemViewModel>();
            if (session != null)
                currentCart = JsonConvert.DeserializeObject<List<CartItemViewModel>>(session);

            foreach(var item in currentCart)
            {
                if(item.ProductId == id)
                {
                    if(quantity <= 0)
                    {
                        currentCart.Remove(item);
                        break;
                    }
                    item.Quantity = quantity;
                }
            }

            HttpContext.Session.SetString(SystemConstants.CartSession, JsonConvert.SerializeObject(currentCart));
            return Ok(currentCart);
        }
        public async Task<IActionResult> GetCartItem()
        {
            var session = HttpContext.Session.GetString(SystemConstants.CartSession);
            List<CartItemViewModel> currentCart = new List<CartItemViewModel>();
            if (session != null)
                currentCart = JsonConvert.DeserializeObject<List<CartItemViewModel>>(session);
            return Ok(currentCart);
        }
        [HttpPost]
        public async Task<IActionResult> Index(CreateOrderRequestValidator requestValid) 
        {
            if (!ModelState.IsValid)
            {
                ViewBag.GetUrlApi = GetUrlImage();
                return View();
            }
            var session = HttpContext.Session.GetString(SystemConstants.CartSession);
            if (session == null)
            {
                return Json(new
                {
                    cart = false
                });
            }
            List<CartItemViewModel> currentCart = new List<CartItemViewModel>();
            if (session != null)
                currentCart = JsonConvert.DeserializeObject<List<CartItemViewModel>>(session);
            var request = new CreateOrderRequest();
            request.ShipAddress = requestValid.ShipAddress;
            request.ShipPhoneNumber = requestValid.ShipPhoneNumber;
            request.ShipEmail = requestValid.ShipEmail;
            request.ShipName = requestValid.ShipName;
            request.Status = SystemConstants.StatusOrder.Inprogess;
            var products = new List<OrderDetailViewModel>();
            foreach(var item in currentCart)
            {
                request.TotalPayment += (item.Price * item.Quantity);
                products.Add(new OrderDetailViewModel()
                {
                    ProductId= item.ProductId,
                    Price= item.Price,
                    Quantity= item.Quantity,
                });
            }
            request.OrderDetails = products;
            var result = await _orderApiClient.CreateOrder(request);
            var sendEmail = await _orderApiClient.SendEmail(request);
            if (sendEmail && result)
            {
                HttpContext.Session.Remove(SystemConstants.CartSession);
                return Json(new
                {
                    status = true
                });
            }
            return Json(new
            {
                status = false
            });
        }

        public async Task<IActionResult> OrderSuccess()
        {
            return View();
        }
        private string GetUrlImage()
        {
            var url = _productApiClient.GetUrlApi();
            var UrlImage = url + "user-content";
            return UrlImage;
        }
    }
}

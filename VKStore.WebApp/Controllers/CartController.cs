using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Newtonsoft.Json;
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
            var url = _productApiClient.GetUrlApi();
            var UrlImage = url + "user-content";
            ViewBag.GetUrlApi = UrlImage;
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
        public async Task<IActionResult> Order(string name, string phoneNumber, string address, string email, string totalPayment) 
        {
            var session = HttpContext.Session.GetString(SystemConstants.CartSession);
            List<CartItemViewModel> currentCart = new List<CartItemViewModel>();
            if (session != null)
                currentCart = JsonConvert.DeserializeObject<List<CartItemViewModel>>(session);
            var request = new CreateOrderRequest();
            request.ShipPhoneNumber = phoneNumber;
            request.ShipAddress = address;
            request.ShipEmail = email;
            request.ShipName = name;
            request.TotalPayment = totalPayment;
            request.Status = SystemConstants.StatusOrder.Inprogess;
            var products = new List<OrderDetailViewModel>();
            foreach(var item in currentCart)
            {
                products.Add(new OrderDetailViewModel()
                {
                    ProductId= item.ProductId,
                    Price= item.Price,
                    Quantity= item.Quantity,
                });
            }
            request.OrderDetails = products;
            var result = await _orderApiClient.CreateOrder(request);
            if (result)
            {
                currentCart = null;
                return RedirectToAction("OrderSuccess", "Cart");
            }
            return View(result);
        }

        public IActionResult OrderSuccess()
        {
            return View();
        }
    }
}

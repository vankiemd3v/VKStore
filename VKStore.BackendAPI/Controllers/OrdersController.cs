using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Text.Json;
using VKStore.ApiIntergration;
using VKStore.Application.Catalog.Orders;
using VKStore.Application.Catalog.Products;
using VKStore.ViewModels.Catalog.Orders;
using VKStore.ViewModels.Catalog.Products;

namespace VKStore.BackendAPI.Controllers
{
    [Route("api/orders")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        public OrdersController(IOrderService orderService, IProductService productService)
        {
            _orderService = orderService;
            _productService = productService;
        }
        [HttpGet("paging")]
        public async Task<IActionResult> Get([FromQuery] GetOrdersPagingRequest request)
        {
            var products = await _orderService.GetAllPaging(request);
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var order = await _orderService.Detail(id);
            return Ok(order);
        }
        [HttpPost("update")]
        public async Task<IActionResult> Post([FromBody]UpdateStatusOrderRequest request)
        {
            var update = await _orderService.UpdateStatusOrder(request);
            return Ok(update);
        }
        [HttpPost("create")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _orderService.CreateOrder(request);
            if (!result)
            {
                return BadRequest(result);
            }
            // Gửi mail cho shop
            var mess = await System.IO.File.ReadAllTextAsync(@"D:\DỰ ÁN\VKStore\VKStore.BackendAPI\wwwroot\html\order.html");
            mess = mess.Replace("{{Name}}", request.ShipName);
            mess = mess.Replace("{{PhoneNumber}}", request.ShipPhoneNumber);
            mess = mess.Replace("{{Email}}", request.ShipEmail);
            mess = mess.Replace("{{Address}}", request.ShipAddress);
            mess = mess.Replace("{{TotalPayment}}", request.TotalPayment);
            var listProductName = new List<ProductCartViewModel>();
            foreach (var item in request.OrderDetails)
            {
               var product = await _productService.GetById(item.ProductId);
               listProductName.Add(new ProductCartViewModel()
               {
                   Name = product.Name,
                   Quantity= item.Quantity,
               });
            }
            var json = JsonSerializer.Serialize(listProductName);
            mess = mess.Replace("{{ListProductName}}", json) ;
            SendEmail(mess, null, "Đơn hàng mới");

            // Gửi mail cho Customer
            var messCus = await System.IO.File.ReadAllTextAsync(@"D:\DỰ ÁN\VKStore\VKStore.BackendAPI\wwwroot\html\orderCustomer.html");
            messCus = messCus.Replace("{{Name}}", request.ShipName);
            messCus = messCus.Replace("{{PhoneNumber}}", request.ShipPhoneNumber);
            messCus = messCus.Replace("{{Email}}", request.ShipEmail);
            messCus = messCus.Replace("{{Address}}", request.ShipAddress);
            messCus = messCus.Replace("{{TotalPayment}}", request.TotalPayment);
            var products = new List<ProductCartViewModel>();
            foreach (var item in request.OrderDetails)
            {
                var product = await _productService.GetById(item.ProductId);
                products.Add(new ProductCartViewModel()
                {
                    Name = product.Name,
                    Quantity = item.Quantity,
                });
            }
            var jsonCus = JsonSerializer.Serialize(products);
            mess = mess.Replace("{{ListProductName}}", jsonCus);
            SendEmail(mess, request.ShipEmail, "Đặt hàng thành công");
            return Ok(result);
        }
        [HttpPost]
        private IActionResult SendEmail(string mess, string? emailCustomer, string subject)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("s2xbladeno1@gmail.com"));
            if (emailCustomer != null)
            {
                email.To.Add(MailboxAddress.Parse(emailCustomer));
            }
            else
            {
                email.To.Add(MailboxAddress.Parse("vankiemd3v@gmail.com"));
            }
            email.Subject = subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = mess
            };
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.office365.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate("s2xbladeno1@gmail.com", "VanKiem06041998");
            smtp.Send(email);
            smtp.Disconnect(true);
            return Ok();
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VKStore.Application.Catalog.Products;
using VKStore.ViewModels.Catalog.Categories;
using VKStore.ViewModels.Catalog.ProductImages;
using VKStore.ViewModels.Catalog.Products;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace VKStore.BackendAPI.Controllers
{
    [Route("api/products")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("paging")]
        public async Task<IActionResult> Get([FromQuery] GetManageProductPagingRequest request)
        {
            var products = await _productService.GetAllPaging(request);
            return Ok(products);
        }
        [HttpGet("category")]
        public async Task<IActionResult> Get()
        {
            var categories = await _productService.GetListCategory();
            return Ok(categories);
        }
        [HttpGet("public-paging")]
        public async Task<IActionResult> Get([FromQuery] GetPublicProductPagingRequest request)
        {
            var products = await _productService.GetAllByCategoryId(request);
            return Ok(products);
        }
        [HttpGet("product-by-category/{categoryId}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromRoute] int categoryId)
        {
            var products = await _productService.GetProductByCategoryId(categoryId);
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetById(id);
            if (product == null)
                return BadRequest("Không tìm thấy sản phẩm");
            return Ok(product);
        }
        [HttpGet("getpublicbyid/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPublicProductById(int id)
        {
            var product = await _productService.GetById(id);
            if (product == null)
                return BadRequest("Không tìm thấy sản phẩm");
            return Ok(product);
        }
        [HttpPost]
        // cho phép nhận kiểu dữ liệu truyền lên là 1 form data
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm]ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var productId = await _productService.Create(request);
            if (productId == 0)
                return BadRequest();
            var product = await _productService.GetById(productId);
            return CreatedAtAction(nameof(GetById), new {id=productId}, product);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromForm] ProductUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.Update(request);
            if (result == 0)
                return BadRequest();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productService.Delete(id);
            if (result == 0)
                return BadRequest();
            return Ok(result);
        }

        // Images
        [HttpGet("{productId}/images/{imageId}")]
        public async Task<IActionResult> GetImageById(int imageId)
        {
            var iamge = await _productService.GetImageById(imageId);
            if (iamge == null)
                return BadRequest("Không tìm thấy hình ảnh");
            return Ok(iamge);
        }
        [HttpPost("{productId}")]
        public async Task<IActionResult> Create(int productId, [FromForm] ProductImageCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var imageId = await _productService.AddImage(productId,request);
            if (imageId == 0)
                return BadRequest();
            var image = await _productService.GetImageById(productId);
            // Trả về  201
            return CreatedAtAction(nameof(GetImageById), new {id=imageId}, image);
        }
        [HttpPut("{productId}/image/{imageId}")]
        public async Task<IActionResult> UpdateImage(int imageId, [FromForm] ProductImageUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.UpdateImage(imageId, request);
            if (result == 0)
                return BadRequest();
            return Ok();
        }
        [HttpDelete("{productId}/image/{imageId}")]
        public async Task<IActionResult> DeleteImage(int imageId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.DeleteImage(imageId);
            if (result == 0)
                return BadRequest();
            return Ok();
        }
        [HttpGet("recent-products/{take}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProduct([FromRoute]int take)
        {
            var products = await _productService.GetListProduct(take, null, null);
            return Ok(products);
        }
        [HttpGet("recent-products/{take}/{categoryId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductByCategoryId([FromRoute] int take, int categoryId)
        {
            var products = await _productService.GetListProduct(take, categoryId, null);
            return Ok(products);
        }
        [HttpGet("search/{keyword}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductByKeyword([FromRoute] string keyword)
        {
            var products = await _productService.GetListProduct(100, null, keyword);
            return Ok(products);
        }
    }
}

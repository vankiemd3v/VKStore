using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VKStore.Application.Catalog.Products;
using VKStore.Application.System.Roles;
using VKStore.Data.Entities;

namespace VKStore.BackendAPI.Controllers
{
    [Route("api/categories")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet()]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAll();
            return Ok(categories);
        }
        [HttpGet("parent")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategoriesParent()
        {
            var categories = await _categoryService.GetCategoriesParent();
            return Ok(categories);
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VKStore.Application.Catalog.Contacts;
using VKStore.ViewModels.Catalog.Contacts;
using VKStore.ViewModels.Catalog.Products;

namespace VKStore.BackendAPI.Controllers
{
    [Route("api/contacts")]
    [ApiController]
    [Authorize]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;
        public ContactsController(IContactService contactService)
        {
            _contactService= contactService;
        }
        [HttpPost("create")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateContact([FromBody]CreateContactRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var create = await _contactService.CreateContact(request);
            return Ok(create);
        }
        [HttpGet("paging")]
        public async Task<IActionResult> Get([FromQuery] GetContactPagingRequest request)
        {
            var products = await _contactService.GetAllContacts(request);
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var products = await _contactService.Detail(id);
            return Ok(products);
        }
    }
}

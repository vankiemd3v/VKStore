using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VKStore.ViewModels.System.Users;
using VKStore.ViewModels.Common;
using VKStore.ApiIntergration;

namespace VKStore.AdminApp.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserApiClient _userApiClient;
        private readonly IConfiguration _configuration;
        private readonly IRoleApiClient _roleApiClient;
        public UserController(IUserApiClient userApiClient, IConfiguration configuration, IRoleApiClient roleApiClient)
        {
            _userApiClient = userApiClient;
            _configuration = configuration;
            _roleApiClient = roleApiClient;
        }
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var session = HttpContext.Session.GetString("Token");
            var request = new GetUserPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            ViewBag.Keyword = keyword;
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            var data = await _userApiClient.GetUsersPagings(request);
            return View(data.ResultObj);
        }
        

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RegisterRequest request)
        {
            // bắt lỗi validation
            if (!ModelState.IsValid)
                return View();
            var result = await _userApiClient.RegisterUser(request);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Thêm người dùng thành công";
                return RedirectToAction("Index", "User");
            }
            // fail trả về 1 error message
            ModelState.AddModelError("", result.Message);
            return View(request);
        }
        public async Task<IActionResult> Update(Guid id)
        {
            var result = await _userApiClient.GetById(id);
            if (result.IsSuccessed)
            {
                var user = result.ResultObj;
                var updateRequest = new UserUpdateRequest()
                {
                    Id= id,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    FullName = user.FullName,
                };
                return View(updateRequest);
            }
            return RedirectToAction("Index","Home");
        }
        [HttpPost]
        public async Task<IActionResult> Update(UserUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View();
            var result = await _userApiClient.UpdateUser(request.Id,request);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Cập nhật người dùng thành công";
                return RedirectToAction("Index", "User");
            }    
                
            ModelState.AddModelError("", result.Message);
            return View(request);
        }
        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _userApiClient.GetById(id);
            return View(result.ResultObj);
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid)
                return View();
            var result = await _userApiClient.DeleteUser(id);
            if (result.IsSuccessed)
            {     
                TempData["result"] = "Xóa người dùng thành công";
                return RedirectToAction("Index", "User");
            }
            ModelState.AddModelError("", result.Message);
            return View();
        }
        public async Task<IActionResult> RoleAssign(Guid id)
        {
            var roleAssignRequest = await GetRoleAssignRequest(id);
            return View(roleAssignRequest);
        }
        [HttpPost]
        public async Task<IActionResult> RoleAssign(RoleAssignRequest request)
        {
            if (!ModelState.IsValid)
                return View();
            var result = await _userApiClient.RoleAssign(request.Id, request);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Phân quyền thành công";
                return RedirectToAction("Index", "User");
            }
            ModelState.AddModelError("", result.Message);
            var roleAssignRequest = await GetRoleAssignRequest(request.Id);
            return View(roleAssignRequest);
        }
        // Tạo 1 cái class dùng chung cho role
        private async Task<RoleAssignRequest> GetRoleAssignRequest(Guid id)
        {
            var user = await _userApiClient.GetById(id);
            var roles = await _roleApiClient.GetAll();
            var roleAssignRequest = new RoleAssignRequest();
            if (roles.ResultObj != null)
            {
                foreach (var role in roles.ResultObj)
                {
                    roleAssignRequest.Roles.Add(new SelectItem()
                    {
                        Id = role.Id.ToString(),
                        Name = role.Name,
                        Selected = user.ResultObj.Roles.Contains(role.Name)
                    });
                }
            }
            roleAssignRequest.Id = id;
            return roleAssignRequest;
        }
    }
}

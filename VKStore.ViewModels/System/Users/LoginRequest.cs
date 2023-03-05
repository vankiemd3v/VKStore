using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKStore.ViewModels.System.Users
{
    public class LoginRequest
    {
        [Required(ErrorMessage ="Nhập tài khoản")]
        public string UserName{ get; set; }
        [Required(ErrorMessage = "Nhập mật khẩu")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKStore.ViewModels.System.Users
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        [Display(Name ="Họ và tên:")]
        public string FullName { get; set; }
        public string Email { get; set; }
        [Display(Name = "Tài khoản:")]
        public string UserName { get; set; }
        [Display(Name = "Số điện thoại:")]
        public string PhoneNumber { get; set; }
        public IList<string> Roles { get; set; }

    }
}

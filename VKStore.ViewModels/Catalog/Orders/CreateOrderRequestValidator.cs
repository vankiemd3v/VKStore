using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKStore.ViewModels.Catalog.Orders
{
    public class CreateOrderRequestValidator
    {
        [Required(ErrorMessage = "Vui lòng nhập họ và tên")]
        [Display(Name = "Khách hàng:")]
        public string ShipName { set; get; }
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
        [Display(Name = "Địa chỉ:")]
        public string ShipAddress { set; get; }
        [Required(ErrorMessage = "Vui lòng nhập Email")]
        [Display(Name = "Email:")]
        public string ShipEmail { set; get; }
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [Display(Name = "Số điện thoại:")]
        public string ShipPhoneNumber { set; get; }
    }
}

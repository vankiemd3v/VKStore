using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKStore.Data.Entities;
using VKStore.Data.Enums;

namespace VKStore.ViewModels.Catalog.Orders
{
    public class OrderViewModel
    {
        public int Id { set; get; }
        [Display(Name="Ngày đặt hàng:")]
        public DateTime OrderDate { set; get; }
        [Display(Name = "Là User?")]
        public Guid UserId { set; get; }
        [Display(Name = "Khách hàng:")]
        public string ShipName { set; get; }
        [Display(Name = "Địa chỉ nhận hàng:")]
        public string ShipAddress { set; get; }
        [Display(Name = "Email:")]
        public string ShipEmail { set; get; }
        [Display(Name = "Số điện thoại:")]
        public string ShipPhoneNumber { set; get; }
        [Display(Name = "Tổng tiền:")]
        public string TotalPayment { set; get; }
        [Display(Name = "Trạng thái:")]
        public string Status { set; get; }
        [Display(Name = "Sản phẩm:")]
        public List<OrderDetailViewModel> OrderDetails { get; set; }
    }
}

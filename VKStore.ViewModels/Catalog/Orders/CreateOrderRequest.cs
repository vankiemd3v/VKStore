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
    public class CreateOrderRequest
    {
        public int Id { set; get; }
        public DateTime OrderDate { set; get; }
        public Guid UserId { set; get; }
        public string ShipName { set; get; }
        public string ShipAddress { set; get; }
        public string ShipEmail { set; get; }
        public string ShipPhoneNumber { set; get; }
        public int TotalPayment { set; get; }
        public string Status { set; get; }
        public List<OrderDetailViewModel> OrderDetails { get; set; }
    }
}

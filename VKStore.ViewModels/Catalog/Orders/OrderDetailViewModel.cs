using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKStore.ViewModels.Catalog.Orders
{
    public class OrderDetailViewModel
    {
        public int OrderId { set; get; }
        public int ProductId { set; get; }
        public int Quantity { set; get; }
        public int Price { set; get; }
        public string? ProductName { get; set; }
    }
}

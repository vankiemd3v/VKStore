using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKStore.ViewModels.Catalog.Orders
{
    public class UpdateStatusOrderRequest
    {
        public int Id { get; set; }
        public string Status { get; set; }
    }
}

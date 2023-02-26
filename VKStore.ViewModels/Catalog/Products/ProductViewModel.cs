using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKStore.ViewModels.Catalog.Products
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string CategoryName { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public string System { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ImagePath { get; set; }
        public string ThumbnailImage { get; set; }
        public int CategoryId { get; set; }
        public string CategoryParentName { get; set; }
    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKStore.ViewModels.Catalog.Products
{
    public class ProductCreateRequest
    {
        [Display(Name="Tên sản phẩm")]
        public string Name { get; set; }
        [Display(Name = "Giá")]
        public int Price { get; set; }
        [Display(Name = "Số lượng")]
        public int Quantity { get; set; }
        [Display(Name = "Mô tả")]
        public string Description { get; set; }
        [Display(Name="Cấu hình")]
        public string System { get; set; }
        [Display(Name = "Danh mục sản phẩm")]
        public int CategoryId { get; set; }
        [Display(Name = "Ảnh")]
        public IFormFile? ThumbnailImage { get; set; }
    }
}

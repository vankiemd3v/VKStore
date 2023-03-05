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
        [Required(ErrorMessage ="Nhập tên sản phẩm")]
        public string Name { get; set; }
        [Display(Name = "Giá")]
        [Required(ErrorMessage = "Nhập giá sản phẩm")]
        public int Price { get; set; }
        [Range(1, int.MaxValue)]
        [Required(ErrorMessage = "Nhập số lượng")]
        public int Quantity { get; set; }
        [Display(Name = "Mô tả")]
        [Required(ErrorMessage = "Nhập mô tả sản phẩm")]
        public string Description { get; set; }
        [Display(Name="Cấu hình")]
        [Required(ErrorMessage = "Nhập cấu hình sản phẩm")]
        public string System { get; set; }
        [Display(Name = "Danh mục sản phẩm")]
        public int CategoryId { get; set; }
        [Display(Name = "Ảnh")]
        public IFormFile? ThumbnailImage { get; set; }
    }
}

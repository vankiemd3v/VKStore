using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKStore.ViewModels.Catalog.ProductImages
{
    public class ProductImageUpdateRequest
    {
        public bool IsDefault { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}

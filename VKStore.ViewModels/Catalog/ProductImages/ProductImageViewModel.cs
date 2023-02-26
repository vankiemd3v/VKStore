using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKStore.ViewModels.Catalog.ProductImages
{
    public class ProductImageViewModel
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string ImagePath { get; set; }
        public bool IsDefault { get; set; }

        public long FileSize { get; set; }
    }
}

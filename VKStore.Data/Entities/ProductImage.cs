﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKStore.Data.Entities
{
    public class ProductImage
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }

        public bool IsDefault { get; set; }

        public long FileSize { get; set; }


        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}

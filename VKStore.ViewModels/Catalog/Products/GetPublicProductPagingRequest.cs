﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKStore.ViewModels.Common;

namespace VKStore.ViewModels.Catalog.Products
{
    public class GetPublicProductPagingRequest: PagingRequestBase
    {
        public int CategoryId { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKStore.ViewModels.Common;

namespace VKStore.ViewModels.Catalog.Slide
{
    public class GetSlidePagingRequest: PagingRequestBase
    {
        public string? Keyword { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKStore.ViewModels.Common;

namespace VKStore.ViewModels.Catalog.Orders
{
    public class GetOrdersPagingRequest: PagingRequestBase
    {
        public string? Keyword { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKStore.ViewModels.Common;

namespace VKStore.ViewModels.Catalog.Contacts
{
    public class GetContactPagingRequest: PagingRequestBase
    {
        public string? Keyword { get; set; }
    }
}

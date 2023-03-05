using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKStore.ViewModels.Catalog.Contacts;
using VKStore.ViewModels.Catalog.Orders;
using VKStore.ViewModels.Common;

namespace VKStore.ApiIntergration
{
    public interface IContactApiClient
    {
        Task<bool> CreateContact(CreateContactRequest request);
        Task<PagedResult<ContactViewModel>> GetContactPagings(GetContactPagingRequest request);
        Task<ContactViewModel> Detail(int id);
    }
}

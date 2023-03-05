using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKStore.ViewModels.Catalog.Contacts;
using VKStore.ViewModels.Common;

namespace VKStore.Application.Catalog.Contacts
{
    public interface IContactService
    {
        Task<bool> CreateContact(CreateContactRequest request);
        Task<PagedResult<ContactViewModel>> GetAllContacts(GetContactPagingRequest request);
        Task<ContactViewModel> Detail(int id);
    }
}

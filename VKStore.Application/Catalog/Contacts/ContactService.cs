using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKStore.Application.Common;
using VKStore.Data.EF;
using VKStore.Data.Entities;
using VKStore.ViewModels.Catalog.Contacts;
using VKStore.ViewModels.Catalog.Products;
using VKStore.ViewModels.Common;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace VKStore.Application.Catalog.Contacts
{
    public class ContactService:IContactService
    {
        private readonly VKStoreDbContext _context;
        private readonly IStorageService _storageService;
        public ContactService(VKStoreDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        public async Task<bool> CreateContact(CreateContactRequest request)
        {
            var contact = new Contact();
            contact.Content = request.Content;
            contact.Name = request.Name;
            contact.Email = request.Email;
            contact.Subject = request.Subject;
            contact.CreatedDate = DateTime.Now;
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ContactViewModel> Detail(int id)
        {
            var contact = await _context.Contacts.FirstOrDefaultAsync(x => x.Id == id);
            var detail = new ContactViewModel()
            {
                Id = id,
                Name = contact.Name,
                Email = contact.Email,
                Subject = contact.Subject,
                CreatedDate = DateTime.Now,
                Content = contact.Content
            };
            return detail;
        }

        public async Task<PagedResult<ContactViewModel>> GetAllContacts(GetContactPagingRequest request)
        {
            var query = from c in _context.Contacts select new { c };
            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.c.Name.Contains(request.Keyword));
            var data = await query.OrderByDescending(x=>x.c.CreatedDate).Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
                            .Select(x => new ContactViewModel()
                            {
                                Id = x.c.Id,
                                Name = x.c.Name,
                                Email = x.c.Email,
                                Subject = x.c.Subject,
                                Content= x.c.Content,
                                CreatedDate = x.c.CreatedDate
                            }).ToListAsync();
            var pagedResult = new PagedResult<ContactViewModel>()
            {
                TotalRecords = query.Count(),
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            return pagedResult;
        }
    }
}

using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKStore.Ultilities.Constants;
using VKStore.ViewModels.Catalog.Contacts;
using VKStore.ViewModels.Catalog.Products;
using VKStore.ViewModels.Common;

namespace VKStore.ApiIntergration
{
    public class ContactApiClient: BaseApiClient, IContactApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;
        public ContactApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor contextAccessor) : base(httpClientFactory, configuration, contextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _contextAccessor = contextAccessor;
        }

        public async Task<bool> CreateContact(CreateContactRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSetting.BaseAddress]);
            var response = await client.PostAsync($"/api/contacts/create", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<bool>(result);
            }
            return JsonConvert.DeserializeObject<bool>(result);
        }

        public async Task<ContactViewModel> Detail(int id)
        {
            var data = await GetAsync<ContactViewModel>($"/api/contacts/{id}");
            return data;
        }

        public async Task<PagedResult<ContactViewModel>> GetContactPagings(GetContactPagingRequest request)
        {
            var data = await GetAsync<PagedResult<ContactViewModel>>($"/api/contacts/paging?pageIndex=" +
                $"{request.PageIndex}&pageSize={request.PageSize}&keyword={request.Keyword}");
            return data;
        }
    }
}

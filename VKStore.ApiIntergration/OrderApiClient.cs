using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using VKStore.Ultilities.Constants;
using VKStore.ViewModels.Catalog.Orders;
using VKStore.ViewModels.Catalog.Products;
using VKStore.ViewModels.Common;
using VKStore.ViewModels.System.Roles;
using VKStore.ViewModels.System.Users;

namespace VKStore.ApiIntergration
{
    public class OrderApiClient : BaseApiClient, IOrderApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;
        public OrderApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor contextAccessor) : base(httpClientFactory, configuration, contextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _contextAccessor = contextAccessor;
        }

        public async Task<bool> CreateOrder(CreateOrderRequest request)
        {
            // convert request sang json
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSetting.BaseAddress]);
            var response = await client.PostAsync($"/api/orders/create", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<bool>(result);
            }
            return JsonConvert.DeserializeObject<bool>(result);
        }

        public async Task<OrderViewModel> Detail(int id)
        {
            var data = await GetAsync<OrderViewModel>($"/api/orders/{id}");
            return data;
        }

        public async Task<PagedResult<OrderViewModel>> GetOrdersPagings(GetOrdersPagingRequest request)
        {
            var data = await GetAsync<PagedResult<OrderViewModel>>($"/api/orders/paging?pageIndex=" +
                $"{request.PageIndex}&pageSize={request.PageSize}&keyword={request.Keyword}");
            return data;
        }

        public async Task<bool> UpdateStatusOrder(UpdateStatusOrderRequest request)
        {
            // convert request sang json
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSetting.BaseAddress]);
            var sessions = _contextAccessor.HttpContext.Session.GetString(SystemConstants.AppSetting.Token);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await client.PostAsync($"/api/orders/update", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<bool>(result);
            }
            return JsonConvert.DeserializeObject<bool>(result);
        }
    }
}

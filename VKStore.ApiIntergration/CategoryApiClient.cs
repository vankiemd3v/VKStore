using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using VKStore.ViewModels.Catalog.Categories;
using VKStore.ViewModels.Catalog.Products;
using VKStore.ViewModels.Common;
using VKStore.ViewModels.System.Roles;
using VKStore.ViewModels.System.Users;

namespace VKStore.ApiIntergration
{
    public class CategoryApiClient : BaseApiClient, ICategoryApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;
        public CategoryApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor contextAccessor) : base(httpClientFactory, configuration, contextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _contextAccessor = contextAccessor;
        }
        public async Task<List<CategoryViewModel>> GetAll()
        {
            var data = await GetListAsync<CategoryViewModel>($"/api/categories");
            return data;
        }

        public async Task<List<CategoryViewModel>> GetCategoriesParent() 
        {
            var data = await GetListAsync<CategoryViewModel>($"/api/categories/parent");
            return data;
        }
    }
}

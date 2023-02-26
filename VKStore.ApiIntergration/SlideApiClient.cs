using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http.Headers;
using System.Security.Policy;
using VKStore.Ultilities.Constants;
using VKStore.ViewModels.Catalog.Categories;
using VKStore.ViewModels.Catalog.Products;
using VKStore.ViewModels.Catalog.Slide;
using VKStore.ViewModels.Common;
using VKStore.ViewModels.System.Roles;
using VKStore.ViewModels.System.Users;

namespace VKStore.ApiIntergration
{
    public class SlideApiClient : BaseApiClient, ISlideApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;
        public SlideApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor contextAccessor)
            : base(httpClientFactory, configuration, contextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _contextAccessor = contextAccessor;
        }
        public async Task<List<SlideViewModel>> GetAll()
        {
            return await GetAsync<List<SlideViewModel>>("/api/slides");
        }
    }
}

using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http.Headers;
using System.Security.Policy;
using VKStore.Ultilities.Constants;
using VKStore.ViewModels.Catalog.Categories;
using VKStore.ViewModels.Catalog.Products;
using VKStore.ViewModels.Common;
using VKStore.ViewModels.System.Roles;
using VKStore.ViewModels.System.Users;

namespace VKStore.ApiIntergration
{
    public class ProductApiClient : BaseApiClient, IProductApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;
        public ProductApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor contextAccessor)
            : base(httpClientFactory, configuration, contextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _contextAccessor = contextAccessor;
        }
        // cần chuyển đổi formfile sang dạng binaryArray, sau đó tạo multipart content
        public async Task<bool> CreateProduct(ProductCreateRequest request)
        {
            var sessions = _contextAccessor.HttpContext.Session.GetString(SystemConstants.AppSetting.Token);
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSetting.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var requestContent = new MultipartFormDataContent();
            // chuyển thumnailImage ra dạng binary, rồi add vào MultipartFormDataContent
            if (request.ThumbnailImage != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.ThumbnailImage.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.ThumbnailImage.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "thumbnailImage", request.ThumbnailImage.FileName);
            }
            requestContent.Add(new StringContent(request.Price.ToString()), "price");
            requestContent.Add(new StringContent(request.Quantity.ToString()), "quantity");
            requestContent.Add(new StringContent(request.Name.ToString()), "name");
            requestContent.Add(new StringContent(request.Description.ToString()), "description");
            requestContent.Add(new StringContent(request.System.ToString()), "system");
            requestContent.Add(new StringContent(request.CategoryId.ToString()), "categoryId");
            var response = await client.PostAsync($"/api/products/", requestContent);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateProduct(ProductUpdateRequest request)
        {
            var sessions = _contextAccessor.HttpContext.Session.GetString(SystemConstants.AppSetting.Token);
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSetting.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var requestContent = new MultipartFormDataContent();
            // chuyển thumnailImage ra dạng binary, rồi add vào MultipartFormDataContent
            if (request.ThumbnailImage != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.ThumbnailImage.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.ThumbnailImage.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "thumbnailImage", request.ThumbnailImage.FileName);
            }
            requestContent.Add(new StringContent(request.Id.ToString()), "id");
            requestContent.Add(new StringContent(request.Price.ToString()), "price");
            requestContent.Add(new StringContent(request.Quantity.ToString()), "quantity");
            requestContent.Add(new StringContent(request.Name.ToString()), "name");
            requestContent.Add(new StringContent(request.Description.ToString()), "description");
            requestContent.Add(new StringContent(request.CategoryId.ToString()), "categoryId");
            requestContent.Add(new StringContent(request.System.ToString()), "system");
            var response = await client.PutAsync($"/api/products", requestContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<ApiResult<List<CategoryViewModel>>> GetListCategory()
        {
            var sessions = _contextAccessor.HttpContext.Session.GetString(SystemConstants.AppSetting.Token);
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSetting.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await client.GetAsync($"/api/products/category");
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                List<CategoryViewModel> myDeserializedObjList = (List<CategoryViewModel>)JsonConvert.DeserializeObject(result, typeof(List<CategoryViewModel>));
                return new ApiSuccessResult<List<CategoryViewModel>>(myDeserializedObjList);
            }
            return JsonConvert.DeserializeObject<ApiErrorResult<List<CategoryViewModel>>>(result);
        }

        public async Task<List<ProductViewModel>> GetListProduct(int take, int? categoryId, string? keyword)
        {
            if(categoryId != null && keyword == null)
            {
                var data = await GetListAsync<ProductViewModel>($"/api/products/recent-products/{take}/{categoryId}");
                return data;
            }
            if (categoryId == null && keyword != null)
            {
                var data = await GetListAsync<ProductViewModel>($"/api/products/search/{keyword}");
                return data;
            }
            if (categoryId != null && keyword != null)
            {
                var data = await GetListAsync<ProductViewModel>($"/api/products/recent-products/{take}/{categoryId}/{keyword}");
                return data;
            }
            else
            {
                var data = await GetListAsync<ProductViewModel>($"/api/products/recent-products/{take}");
                return data;
            }
        }
        //public async Task<List<ProductViewModel>> GetListProductByCategory(int categoryId)
        //{
        //    var data = await GetListAsync<ProductViewModel>($"/api/products/product-by-category/{categoryId}");
        //    return data;
        //}
        public async Task<PagedResult<ProductViewModel>> GetProductsPagings(GetManageProductPagingRequest request)
        {
            var data = await GetAsync<PagedResult<ProductViewModel>>($"/api/products/paging?pageIndex=" +
                $"{request.PageIndex}&pageSize={request.PageSize}&keyword={request.Keyword}&categoryId={request.CategoryId}");
            return data;
        }
        public string GetUrlApi()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSetting.BaseAddress]);
            return client.BaseAddress.ToString();
        }

        public async Task<ProductViewModel> GetProduct(int id)
        {
            var sessions = _contextAccessor.HttpContext.Session.GetString(SystemConstants.AppSetting.Token);
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSetting.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await client.GetAsync($"/api/products/{id}");
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ProductViewModel>(result);
            }
            return JsonConvert.DeserializeObject<ProductViewModel>(result);
        }
        public async Task<ProductViewModel> GetPublicProductById(int id)
        {
            
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSetting.BaseAddress]);
            var response = await client.GetAsync($"/api/products/getpublicbyid/{id}");
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ProductViewModel>(result);
            }
            return JsonConvert.DeserializeObject<ProductViewModel>(result);
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSetting.BaseAddress]);
            var sessions = _contextAccessor.HttpContext.Session.GetString(SystemConstants.AppSetting.Token);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await client.DeleteAsync($"api/products/{id}");
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<bool>(result);
            }
            return JsonConvert.DeserializeObject<bool>(result);
        }
    }
}

using Pizza_Ordering.web.Services.Cpntracts;
using PizzaOrdering.Models.Dtos;
using System.Net.Http;
using System.Net.Http.Json;

namespace Pizza_Ordering.web.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient ) 
        {
         _httpClient = httpClient;
        }
        public async Task<ExtraItemDto> GetExtraItem(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Product/{id}/GetExtraitembyid");
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(ExtraItemDto);
                    }
                    return await response.Content.ReadFromJsonAsync<ExtraItemDto>();

                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<ExtraItemDto>> GetExtraItems()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Product/GetExtraItems");
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<ExtraItemDto>();
                    }
                    return await response.Content.ReadFromJsonAsync<IEnumerable<ExtraItemDto>>();

                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<ProductMenuDto>> GetItemByCategory(int categoryId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Product/{categoryId}/GetItemsByCategory");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<ProductMenuDto>();
                    }
                    return await response.Content.ReadFromJsonAsync<IEnumerable<ProductMenuDto>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http Status Code - {response.StatusCode} Message - {message}");
                }
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

        public async Task<IEnumerable<ProductCategoryDto>> GetProductCategories()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Product/GetProductCategories");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<ProductCategoryDto>();
                    }
                    return await response.Content.ReadFromJsonAsync<IEnumerable<ProductCategoryDto>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http Status Code - {response.StatusCode} Message - {message}");
                }
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

    

    public async Task<ProductMenuDto> GetProductsItem(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Product/{id}");
                if (response.IsSuccessStatusCode)
                {
                    if(response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(ProductMenuDto);
                    }
                    return await response.Content.ReadFromJsonAsync<ProductMenuDto>();

                }
                else 
                { 
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<ProductMenuDto>> GetProductsItems()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Product");
                if(response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<ProductMenuDto>();
                    }
                    return await response.Content.ReadFromJsonAsync<IEnumerable<ProductMenuDto>>();

                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

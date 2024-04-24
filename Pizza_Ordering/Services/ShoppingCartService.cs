using Newtonsoft.Json;
using Pizza_Ordering.web.Services.Contracts;
using PizzaOrdering.Models.Dtos;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;

namespace Pizza_Ordering.web.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly HttpClient _httpClient;

        public ShoppingCartService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CartItemDto> AddItemToCart(CartItemToAddDto cartItemToAdd)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync<CartItemToAddDto>("api/ShoppingCart/AddToCart", cartItemToAdd);
                if (response.IsSuccessStatusCode)
                {
                    if(response.StatusCode ==System.Net.HttpStatusCode.NoContent)
                    {
                        return default(CartItemDto);

                    }
                    var cartItemDto = await response.Content.ReadFromJsonAsync<CartItemDto>();

                    return cartItemDto;
                }
                else
                {
                    var message = response.Content.ReadAsStringAsync();
                    throw new Exception ($"Http status:{response.StatusCode}, message:{message}");

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<CartItemDto> DeleteItem(int id)
        {
            try
            {
                var respons = await _httpClient.DeleteAsync($"api/ShoppingCart/{id}");
                if(respons.IsSuccessStatusCode)
                {
                    return await respons.Content.ReadFromJsonAsync<CartItemDto>();

                }
                return default(CartItemDto);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<CartItemDto>> GetCartItems(int userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/ShoppingCart/{userId}/GetItems");
                if(response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<CartItemDto>().ToList();

                    }
                    return await response.Content.ReadFromJsonAsync<List<CartItemDto>>();

                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status code: {response.StatusCode} Message: {message}");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }



        public async Task<CartItemDto> UpdateQty(CartItemQtyUpdateDto cartItemQtyUpdateDto)
        {
            try
            {
                var jsonRequest = JsonConvert.SerializeObject(cartItemQtyUpdateDto);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json-patch+json");

                var response = await _httpClient.PatchAsync($"api/ShoppingCart/{cartItemQtyUpdateDto.CartItemId}", content);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<CartItemDto>();
                }
                return null;

            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }
    }
}

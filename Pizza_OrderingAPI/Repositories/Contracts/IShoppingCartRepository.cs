using Pizza_OrderingAPI.Entities;
using PizzaOrdering.Models.Dtos;

namespace Pizza_OrderingAPI.Repositories.Contracts
{
    public interface IShoppingCartRepository
    {
        Task<CartItem> AddItem(CartItemToAddDto cartItemToAddDto);
        Task<CartItem> UpdateQty(int id , CartItemQtyUpdateDto cartItemQtyUpdateDto);
        Task <CartItem> DeleteItem(int id);
        Task<CartItem> GetItemById(int id);
        Task<IEnumerable<CartItem>> GetItemsByUser(int userId);


    }
}

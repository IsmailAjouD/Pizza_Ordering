using Pizza_OrderingAPI.Entities;
using PizzaOrdering.Models.Dtos;

namespace Pizza_OrderingAPI.Repositories.Contracts
{
    public interface IShoppingCartRepository
    {
        Task<CartItem> GetItemById(int id);
        Task<CartItem> AddItem(CartItemToAddDto cartItemToAddDto);
        Task<CartItem> UpdateQty(int id , CartItemQtyUpdateDto cartItemQtyUpdateDto);
        Task <CartItem> DeleteItem(int id);
        Task<IEnumerable<CartItem>> GetItemsByUser(int userId);

        Task<CartItem>DeletItemFromCartItem(CartIemDeletItemDto cartIemDeletitemDto);

        Task<CartItem> DeletExstraItemFromCartItem(int cartItemId, int extraItemId);
    }
}

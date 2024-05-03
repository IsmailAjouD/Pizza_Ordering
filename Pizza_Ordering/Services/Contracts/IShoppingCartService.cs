using PizzaOrdering.Models.Dtos;

namespace Pizza_Ordering.web.Services.Contracts
{
    public interface IShoppingCartService
    {
        Task<List<CartItemDto>> GetCartItems(int userId);
        Task<CartItemDto> AddItemToCart(CartItemToAddDto cartItemToAdd);
        Task<CartItemDto> UpdateQty(CartItemQtyUpdateDto cartItemQtyUpdateDto);
        Task<CartIemDeletItemDto> UpdatRemovableItem(CartIemDeletItemDto  cartIemDeletItemDto);
        Task<CartItemDto> DeleteItem(int id);
        Task<List<CartItemDto>> GetCartItembyId(int id);
        Task<CartItemDto> DeletExtraItem(CartItemForDeletExtraItemDto cartItemForDelet);

        
    }
}

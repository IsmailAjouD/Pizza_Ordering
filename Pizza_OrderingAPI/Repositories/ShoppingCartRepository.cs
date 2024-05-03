using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Pizza_OrderingAPI.Data;
using Pizza_OrderingAPI.Entities;
using Pizza_OrderingAPI.Repositories.Contracts;
using PizzaOrdering.Models.Dtos;

namespace Pizza_OrderingAPI.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ShopOnlineDbContext _shopOnlineDbContext;

        private readonly IProductRepository _productRepository;

        public ShoppingCartRepository( ShopOnlineDbContext shopOnlineDbContext, IProductRepository productRepository)
        {
            _shopOnlineDbContext = shopOnlineDbContext;
            _productRepository = productRepository;

        }
        private async Task<bool> CartItemExists(int cardId, int productId,int productSize)
        {
            return await _shopOnlineDbContext.CartItems.AnyAsync(c=>c.ProductId == productId && c.CartId==cardId && c.SizeId == productSize);
        }

        public async Task<CartItem> AddItem(CartItemToAddDto cartItemToAddDto)
        {
            if (!await CartItemExists(cartItemToAddDto.CartId, cartItemToAddDto.ProductId, cartItemToAddDto.ProductSize))
            {
                // Create a new CartItem instance
                var item = new CartItem
                {
                    CartId = cartItemToAddDto.CartId,
                    ProductId = cartItemToAddDto.ProductId,
                    Quantity = cartItemToAddDto.Quntity,
                    SizeId = cartItemToAddDto.ProductSize,
                    Price = cartItemToAddDto.Price,
                    ExtraItemsId = cartItemToAddDto.ExtraItemIds,
                    RemovableItems  = cartItemToAddDto.RemovableItems,
                };

       
                // Add the new CartItem to the database
                var result = await _shopOnlineDbContext.CartItems.AddAsync(item);
                await _shopOnlineDbContext.SaveChangesAsync();

                // Return the added CartItem
                return result.Entity;
            }
            // Handle case where the cart item already exists
            return null;
        }
        public async Task<CartItem> DeleteItem(int id)
        {
            var item = await _shopOnlineDbContext.CartItems.FindAsync(id);

            if (item != null)
            {
                _shopOnlineDbContext.CartItems.Remove(item);
                await _shopOnlineDbContext.SaveChangesAsync();
            }

            return item;
        }

        public async Task<CartItem> GetItemById(int id)
        {
            return await (from cart in _shopOnlineDbContext.Carts join cartItem in _shopOnlineDbContext.CartItems on cart.Id equals cartItem.CartId where cartItem.CartId == id select new CartItem()
            {
                CartId = cartItem.CartId,
                ProductId= cartItem.ProductId,
                CartItemId= cartItem.ProductId,
                Quantity = cartItem.Quantity
            }).SingleOrDefaultAsync();
        }  
        public async Task<IEnumerable<CartItem>> GetItemsByUser(int userId)
        {
            return await (from cart in this._shopOnlineDbContext.Carts
                          join cartItem in this._shopOnlineDbContext.CartItems on cart.Id equals cartItem.CartId where cart.UserId == userId
                          select new CartItem
                          {
                              CartId = cartItem.CartId,
                              CartItemId = cartItem.CartItemId,
                              ProductId = cartItem.ProductId,
                              Quantity = cartItem.Quantity,
                              SizeId= cartItem.SizeId,
                              ExtraItemsId= cartItem.ExtraItemsId,
                              Price =cartItem.Price,
                              RemovableItems=cartItem.RemovableItems,
                              
                              //Pizza=cartItem.Pizza
                              
                          }).ToListAsync();
        }
        //public async Task<CartItem> UpdateQty(int id, CartItemQtyUpdateDto cartItemQtyUpdateDto)
        //{
        //    try
        //    {
        //        var item = await _shopOnlineDbContext.CartItems.FindAsync(id);
        //        if (item == null)
        //        {
        //            return null; // Cart item not found
        //        }

        //        // Update the quantity and price of the cart item
        //        item.Quantity = cartItemQtyUpdateDto.Qty;
        //        item.Price = cartItemQtyUpdateDto.Price;

        //        // Retrieve the existing extra item from the database
        //        var extraitem = await _shopOnlineDbContext.ExtraItems.FindAsync(cartItemQtyUpdateDto.ExtraItemsId);
        //        if (extraitem != null)
        //        {
        //            foreach (var extraitem2 in extraitem)
        //            {

        //            }
        //            item.ExtraItemsId = cartItemQtyUpdateDto.ExtraItemsId;
        //            // Update the reference to the extra item associated with the cart item
        //        }

        //        // Save the changes to the database
        //        await _shopOnlineDbContext.SaveChangesAsync();

        //        return item;
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle any exceptions
        //        Console.WriteLine($"Error updating cart item: {ex.Message}");
        //        throw;
        //    }
        //}
        public async Task<CartItem> UpdateQty(int id, CartItemQtyUpdateDto cartItemQtyUpdateDto)
        {
            try
            {
                // Hent den eksisterende vare fra databasen
                var item = await _shopOnlineDbContext.CartItems.FindAsync(id);
                if (item == null)
                {
                    return null; // Varen blev ikke fundet
                }

                // Opdater mængden og prisen på varen
                item.Quantity = cartItemQtyUpdateDto.Qty;
                item.Price = cartItemQtyUpdateDto.Price;
                //foreach (var extraItemId in cartItemQtyUpdateDto.ExtraItemsId)
                //{
                //    var extraItem = await _shopOnlineDbContext.ExtraItems.FindAsync(extraItemId);
                //    if (extraItem != null)
                //    {
                //        item.Price += extraItem.Price;
                //    }
                //}

                // Gem ændringerne i databasen
                await _shopOnlineDbContext.SaveChangesAsync();

                return item;
            }
            catch (Exception ex)
            {
                // Håndter fejl, hvis det er relevant
                throw;
            }
        }

        public async Task<CartItem> DeletExstraItemFromCartItem(int cartItemId, int extraItemId)
        {
            try
            {
                var cartItem = await _shopOnlineDbContext.CartItems.FindAsync(cartItemId);
                if (cartItem == null)
                {
                    return null;
                }

                // Split ExtraItemsId string into individual ids
                var extraItemIds = cartItem.ExtraItemsId.Split(',').ToList();

                // Check if extraItemId exists in the list
                if (extraItemIds == null || !extraItemIds.Contains(extraItemId.ToString()))
                {
                    return cartItem; // Extra item not found in the list
                }

                // Remove extraItemId from the list
                extraItemIds.Remove(extraItemId.ToString());

                // Update the ExtraItemsId string
                cartItem.ExtraItemsId = string.Join(",", extraItemIds);

                var extraItem = await _shopOnlineDbContext.ExtraItems.FindAsync(extraItemId);
                if (extraItem != null)
                {
                    // Subtract the extra item's price from the cart item's total price
                    cartItem.Price -= extraItem.Price;
                }

                // Save changes to the database
                await _shopOnlineDbContext.SaveChangesAsync();

                return cartItem;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CartItem> DeletItemFromCartItem(CartIemDeletItemDto cartIemDeletitemDto)
        {
            var item = await _shopOnlineDbContext.CartItems.FindAsync(cartIemDeletitemDto.CartItemId);
            if(item != null)
            {
                var removableItemList = item.RemovableItems?.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList() ?? new List<string>();
                removableItemList.Remove(cartIemDeletitemDto.RemovableItems);
                item.RemovableItems = string.Join(" ", removableItemList);
                await _shopOnlineDbContext.SaveChangesAsync();
                return item;
                
            }
            return null;
        }

    }
}

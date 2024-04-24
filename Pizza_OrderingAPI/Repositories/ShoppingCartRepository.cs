using Microsoft.EntityFrameworkCore;
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
        //public async Task<CartItem> AddItem(CartItemToAddDto cartItemToAddDto)
        //{
        //    if(await CartItemExists(cartItemToAddDto.CartId,cartItemToAddDto.ProductId,cartItemToAddDto.ProductSize)==false)
        //    {
        //      var item = await (from product in _shopOnlineDbContext.ProductMenus where product.PizzaId==cartItemToAddDto.ProductId select new CartItem()
        //      {
        //          CartId= cartItemToAddDto.CartId,
        //          ProductId= cartItemToAddDto.ProductId,
        //          Quantity=cartItemToAddDto.Quntity,
        //          SizeId = cartItemToAddDto.ProductSize,
        //          TotalPrice= cartItemToAddDto.TotalPrice,


        //      }).SingleOrDefaultAsync();
        //        if(item!=null)
        //        {
        //            var result = await _shopOnlineDbContext.CartItems.AddAsync(item);
        //            await _shopOnlineDbContext.SaveChangesAsync();
        //            return result.Entity;
        //        }

        //    }
        //    return null;
        //}

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
                    TotalPrice = cartItemToAddDto.TotalPrice,
                    ExtraItemsId = cartItemToAddDto.ExtraItemIds,
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
                              SizeId= cartItem.SizeId
                              //Pizza=cartItem.Pizza
                              
                          }).ToListAsync();
        }
        public async Task<CartItem> UpdateQty(int id, CartItemQtyUpdateDto cartItemQtyUpdateDto)
        {
            var item = await _shopOnlineDbContext.CartItems.FindAsync(id);
            if (item != null)
            {
                item.Quantity = cartItemQtyUpdateDto.Qty;
                await _shopOnlineDbContext.SaveChangesAsync();
                return item;
            }
            return null;
        }
     
    }
}

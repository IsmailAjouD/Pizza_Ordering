using Microsoft.EntityFrameworkCore;
using Pizza_Ordaring.Infrastructure.Context;
using Pizza_Ordaring.Infrastructure.Entities;
using Pizza_Ordaring.Infrastructure.Interface;
using Pizza_Ordaring.Shared.Models;
using System.Linq.Expressions;


namespace Pizza_Ordaring.Infrastructure.Repositories
{
    public class ShoppingCartRepository : GenericRepository<CartItem>
    {
        private static ShoppingCartRepository _instance;
        public static ShoppingCartRepository Instance {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }
                _instance= new ShoppingCartRepository(Pizza_OrdaringDbContext.Instance);
                return _instance;
            }
        }
        public new Pizza_OrdaringDbContext dbContext { get; set; }
        public ShoppingCartRepository(Pizza_OrdaringDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
       
       public override IEnumerable<CartItem> GetAll()
        {
            return dbContext.CartItems.ToList();
        }
        public async Task<IEnumerable<CartItem>> GetCartItemsbyUserId(int userId)
        {
            return await (from cart in this.dbContext.Carts
                          join cartItem in this.dbContext.CartItems on cart.Id equals cartItem.CartId
                          where cart.UserId == userId
                          select new CartItem
                          {
                              CartId = cartItem.CartId,
                              CartItemId = cartItem.CartItemId,
                              ProductId = cartItem.ProductId,
                              Quantity = cartItem.Quantity,
                              SizeId = cartItem.SizeId,
                              ExtraItemsId = cartItem.ExtraItemsId,
                              Price = cartItem.Price,
                              RemovableItems = cartItem.RemovableItems,
                              
                              //Pizza=cartItem.Pizza

                          }).ToListAsync();
        }



    }
}

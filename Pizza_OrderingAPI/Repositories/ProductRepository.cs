using Microsoft.EntityFrameworkCore;
using Pizza_OrderingAPI.Data;
using Pizza_OrderingAPI.Entities;
using Pizza_OrderingAPI.Repositories.Contracts;

namespace Pizza_OrderingAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopOnlineDbContext _shopOnlineDbContext;

        public ProductRepository(ShopOnlineDbContext pizza_shopOnlineDbContext)
        {
            _shopOnlineDbContext = pizza_shopOnlineDbContext;
        }
        public async Task<IEnumerable<ProductMenu>> GetItems()
        {
          var products = await _shopOnlineDbContext.ProductMenus.Include(p=>p.ItemCategory).Include(s=>s.Sizes).ToListAsync();
            return products;

        }
        public async Task<IEnumerable<ItemCategory>> GetItemCategories()
        {
           var categories = await _shopOnlineDbContext.ItemCategory.ToListAsync();
            return categories;
        }
        public async Task<ProductMenu> GetItem(int id)
        {
            var product = await _shopOnlineDbContext.ProductMenus
             .Include(p => p.ItemCategory) // Include the navigation property
             .Include(s => s.Sizes)
             .SingleOrDefaultAsync(p => p.PizzaId == id);
            return product;
        }
        public async Task<ItemCategory> GetCategory(int id)
        {
            var category = await _shopOnlineDbContext.ItemCategory.SingleOrDefaultAsync(c => c.Id == id);
            return category;
        }
        public async Task<IEnumerable<ProductMenu>> GetItemsByCategory(int id)
        {
            var products = await _shopOnlineDbContext.ProductMenus.Include(p=>p.ItemCategory).Include(s=>s.Sizes).Where(p=>p.CategoryId==id).ToListAsync();
            return products;
        }

        public async Task<IEnumerable<ProductSize>> GetSizes()
        {
            var sizes = await _shopOnlineDbContext.PizzaSizes.ToListAsync();
            return sizes;
        }
        public async Task<ProductSize> GetItembySizeId(int sizeId)
        {
                //var Sizeid = await _shopOnlineDbContext.PizzaSizes.SingleOrDefaultAsync(s=>s.PizzaId==sizeId);
                //return Sizeid;
            var product = await _shopOnlineDbContext.PizzaSizes
            .Include(p => p.Pizza)       
            .SingleOrDefaultAsync(p => p.SizeId == sizeId);
            return product;

        }
     
        public async Task<ExtraItem> GetExtraItem(int id)
        {
            var extraitem = await _shopOnlineDbContext.ExtraItems.SingleOrDefaultAsync(e=>e.ExtraItemId==id);
            return extraitem;
        }

        public async Task<IEnumerable<ExtraItem>> GetExtraItems()
        {
            var extraItems = await _shopOnlineDbContext.ExtraItems.ToListAsync();
            return extraItems;
        }

        public async Task<List<ExtraItem>> GetExtraItemsbyListid(string extraItemIds)
        {
           

            var ids = extraItemIds.Split(',').Select(int.Parse).ToList();
            var extraItems = await _shopOnlineDbContext.ExtraItems.Where(e => ids.Contains(e.ExtraItemId)).ToListAsync();
            return extraItems;


        }

      

     
    }
}

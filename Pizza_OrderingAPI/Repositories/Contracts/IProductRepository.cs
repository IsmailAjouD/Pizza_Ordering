using Pizza_OrderingAPI.Entities;
using PizzaOrdering.Models.Dtos;

namespace Pizza_OrderingAPI.Repositories.Contracts
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductMenu>> GetItems();
        Task<IEnumerable<ItemCategory>> GetItemCategories();
        Task<ProductMenu> GetItem(int id);
        Task<ItemCategory> GetCategory(int id);
        Task<IEnumerable<ProductMenu>> GetItemsByCategory(int id);
        Task<List<ExtraItem>> GetExtraItemsbyListid(string extraItemIds);
        Task<IEnumerable<ExtraItem>> GetExtraItems();
        Task<ExtraItem> GetExtraItem(int id);
        Task<IEnumerable<ProductSize>> GetSizes();
        Task<ProductSize> GetItembySizeId(int id);
        //Task<List<ExtraItem>> GetExtraItems(List<int> extraItemIds);
        //Task GetExtraItem(List<ExtraItemDto> extraItemIds);
        //Task<IEnumerable<ProductSize>> GetISizeIdByUserId(int pizzaId);
    }
}

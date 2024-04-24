using PizzaOrdering.Models.Dtos;

namespace Pizza_Ordering.web.Services.Cpntracts
{
    public interface IProductService
    {
       Task<IEnumerable<ProductMenuDto>> GetProductsItems();
        Task<ProductMenuDto> GetProductsItem(int id);
        Task<IEnumerable<ProductCategoryDto>> GetProductCategories();
        Task<IEnumerable<ProductMenuDto>> GetItemByCategory(int categoryId);
        Task<IEnumerable<ExtraItemDto>> GetExtraItems();
        Task<ExtraItemDto> GetExtraItem(int id);
    }
}

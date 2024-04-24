using Microsoft.AspNetCore.Components;
using Pizza_Ordering.web.Services.Cpntracts;
using PizzaOrdering.Models.Dtos;

namespace Pizza_Ordering.web.Pages.ProductComponents
{
    public class ProductsByCategoryBase : ComponentBase
    {
        [Parameter]
        public int CategoryId { get; set; }
        [Inject]
        public IProductService ProductService { get; set; }

   

        public IEnumerable<ProductMenuDto> Products { get; set; }
        public string CategoryName { get; set; }
        public string ErrorMessage { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            try
            {

                Products = await ProductService.GetItemByCategory(CategoryId);

                if (Products != null && Products.Count() > 0)
                {
                    var productDto = Products.FirstOrDefault(p => p.CategoryId == CategoryId);

                    if (productDto != null)
                    {
                        CategoryName = productDto.CategoryName;
                    }

                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        //private async Task<IEnumerable<ProductMenuDto>> GetProductCollectionByCategoryId(int categoryId)
        //{
        //    var productCollection = await ManageProductsLocalStorageService.GetCollection();

        //    if (productCollection != null)
        //    {
        //        return productCollection.Where(p => p.CategoryId == categoryId);
        //    }
        //    else
        //    {
        //        return await ProductService.GetItemsByCategory(categoryId);
        //    }

        //}
    }
}

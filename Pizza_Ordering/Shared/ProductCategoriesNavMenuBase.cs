using Microsoft.AspNetCore.Components;
using Pizza_Ordering.web.Services.Cpntracts;
using PizzaOrdering.Models.Dtos;

namespace Pizza_Ordering.web.Shared
{
    public class ProductCategoriesNavMenuBase :ComponentBase
    {
        [Inject]
        public IProductService ProductService { get; set; }

        public IEnumerable<ProductCategoryDto> ProductCategoryDtos { get; set; }

        public string ErrorMessage { get; set; }

        public bool isActiv { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                ProductCategoryDtos = await ProductService.GetProductCategories();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}

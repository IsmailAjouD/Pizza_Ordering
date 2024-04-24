using BlazorDialog;
using Microsoft.AspNetCore.Components;
using Pizza_Ordering.web.Pages.ProductComponents;
using Pizza_Ordering.web.Services.Cpntracts;
using PizzaOrdering.Models.Dtos;
using System.Linq;
using System.Text.Json;

namespace Pizza_Ordering.web.Pages
{
    public class MenuBase :ComponentBase
    {
      
        [Inject]
        public IProductService ProductService { get; set; }
        public IEnumerable<ProductMenuDto> Products { get; set; }
        public string ErrorMeassage { get; private set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Products = await ProductService.GetProductsItems();
            }



        protected IOrderedEnumerable<IGrouping<int?, ProductMenuDto>> GetGroupedProductsByCategory()
        {
            if (Products == null)
            {
                return Enumerable.Empty<IGrouping<int?, ProductMenuDto>>().OrderBy(g => g.Key);
            }

            return from product in Products
                   group product by product.CategoryId into prodByCatGroup
                   orderby prodByCatGroup.Key
                   select prodByCatGroup;
        }
        protected string GetCategoryName(IGrouping<int?, ProductMenuDto> groupedProductDtos)
        {
            return groupedProductDtos.FirstOrDefault(pg => pg.CategoryId == groupedProductDtos.Key).CategoryName;
        }

    


    }
}









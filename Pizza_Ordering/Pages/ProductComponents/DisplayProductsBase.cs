using BlazorDialog;
using Microsoft.AspNetCore.Components;
using PizzaOrdering.Models.Dtos;

namespace Pizza_Ordering.web.Pages.ProductComponents
{
    public class DisplayProductsBase : ComponentBase
    {
        [Inject]
        public IBlazorDialogService DialogService { get; set; }

        [Parameter]
        public IEnumerable<ProductMenuDto> Products { get; set; }


        string dialogResult = null;
        bool isCentered;
        DialogSize size;
        DialogAnimation animation;
        public async Task ButtonOnClick(int products)
        {
            dialogResult = null;

            dialogResult = await DialogService.ShowComponentAsDialog<string>(new ComponentAsDialogOptions(typeof(ProductDetails))
            {
                Animation = animation,
                Size = size,
                Centered = isCentered,
                Parameters = new Dictionary<string, object>
            {
                 { nameof(ProductDetails.Id), products }
            }
            });
        }

    }
}

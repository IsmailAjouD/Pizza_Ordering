
using BlazorDialog;
using Microsoft.AspNetCore.Components;
using Pizza_Ordering.web.Services;
using Pizza_Ordering.web.Services.Contracts;
using Pizza_Ordering.web.Services.Cpntracts;
using PizzaOrdering.Models.Dtos;
using System.Linq;

namespace Pizza_Ordering.web.Pages.ProductComponents
{
    public class ProductDetailsBase : ComponentBase
    {
        // Parameters and Injected Services
        [Parameter] public int Id { get; set; }
        [CascadingParameter(Name = "ParentDialog")] public Dialog Dialog { get; set; }
        [Inject] public IProductService ProductService { get; set; }

        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }

        // Product and Extra Items Properties
        public ProductMenuDto Product { get; set; }
        public IEnumerable<ExtraItemDto> ExtraItems { get; set; }
        public ExtraItemDto ExtraItem { get; set; }

        // Other Properties
        public List<string> Descriptions { get; set; }

        public string ErrorMessage { get; set; }
        public int SelectedSizeId { get; set; }

        protected List<bool> isCheckedList = new List<bool>();
        public int Quantity { get; set; } = 1;
        public decimal TotalPrice { get; set; }
        public bool ShowText { get; set; } = false;

        // OnInitializedAsync method
        protected override async Task OnInitializedAsync()
        {
            try
            {
                Product = await ProductService.GetProductsItem(Id);
                ExtraItems = await ProductService.GetExtraItems();
                Quantity = 1;
                SetDefaultSize();
                GetTotalPrice();
                Descriptions = GetDescriptionAsSplit().ToList();
                isCheckedList = Enumerable.Repeat(true, Descriptions.Count).ToList();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        // Utility Methods
        protected string[] GetDescriptionAsSplit()
        {
            if (!string.IsNullOrEmpty(Product.Description))
            {
                return Product.Description.Split(',');
            }
            return new string[0];
        }
        private void SetDefaultSize()
        {
            var smallSize = Product.Sizes.FirstOrDefault(s => s.Name == "Small");
            if (smallSize != null)
                SelectedSizeId = smallSize.SizeId;
        }

        private void GetTotalPrice()
        {
            var selectedSize = Product.Sizes.FirstOrDefault(s => s.SizeId == SelectedSizeId);
            var totalPrice = selectedSize != null ? (selectedSize.Price * Quantity) : 0;

            foreach (var itemId in SelectedExtraItemIds)
            {
                var extraItem = ExtraItems.FirstOrDefault(item => item.ExtraItemId == itemId);
                if (extraItem != null)
                {
                    totalPrice += extraItem.Price;
                }
            }

            TotalPrice = totalPrice;
            StateHasChanged();
        }
        //public ExtraItemDto ExtraProductItems { get; set; }


        // Event Handlers
        public void ToggleVisibility()
        {
            ShowText = !ShowText;
        }

        public void Increment()
        {
            Quantity++;
            GetTotalPrice();
        }

        public void Decrement()
        {
            if (Quantity > 1)
            {
                Quantity--;
                GetTotalPrice();
            }
        }

        public void UpdatePrice(int itemId)
        {
            SelectedSizeId = itemId;
            GetTotalPrice();
        }

        public void UpdatePriceWithExtraItem(int itemId)
        {
            // Find the extra item in the list of extra items
            var extraItem = ExtraItems.FirstOrDefault(item => item.ExtraItemId == itemId);

            if (extraItem != null)
            {
                // Check if the extra item is already selected
                if (SelectedExtraItemIds.Contains(itemId))
                {
                    // If it is selected, remove it from the list
                    SelectedExtraItemIds.Remove(itemId);
                }
                else
                {
                    // If it's not selected, add it to the list
                    SelectedExtraItemIds.Add(itemId);
                }

                // Recalculate the total price
                GetTotalPrice();
            }
        }

        // Helper Methods
        public bool IsChecked(string sizeName) => sizeName == "Small";

        public decimal GetPrice(int sizeId)
        {
            if (Product != null && Product.Sizes != null)
            {
                var size = Product.Sizes.FirstOrDefault(s => s.SizeId == sizeId);
                if (size != null)
                {
                    return size.Price;
                }
            }
            return 0; 
        }
        private List<CartItemDto> ShoppingCartItems { get; set; }

      public  List<int> SelectedExtraItemIds = new List<int>();

        private async void GetExtraItemById(int id)
        {
            ExtraItem = await ProductService.GetExtraItem(id);
            if (ExtraItem != null)
            {
                TotalPrice += ExtraItem.Price;
                Console.WriteLine(ExtraItem.ExtraItemId);

                StateHasChanged();
            }
            //SelectedExtraItems = ExtraItem;

        }
        public async Task AddToCart_Click()

        {
            try
            {

                var cartItemToAddDto = new CartItemToAddDto
                {
                    CartId= HardCoded.CartId,
                    ProductId = Product.PizzaId,
                    Quntity = Quantity,
                    ProductSize= SelectedSizeId,
                    TotalPrice = TotalPrice,
                    ExtraItemIds = string.Join(",", SelectedExtraItemIds)
                };
                //if (SelectedExtraItems != null && SelectedExtraItems.Any())
                //{
                //    cartItemToAddDto.ExtraItemIds.AddRange((IEnumerable<ExtraItemDto>)SelectedExtraItems.Select(item => item.ExtraItemId));
                //}
                var cartItemDto = await ShoppingCartService.AddItemToCart(cartItemToAddDto);
                if (cartItemDto != null)
                {
                    if (ShoppingCartItems == null)
                    {
                        ShoppingCartItems = new List<CartItemDto>();
                    }
                    ShoppingCartItems.Add(cartItemDto);
                   
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        //private async Task SaveCartItemToDatabase(CartItemDto cartItemDto)
        //{
        //    try
        //    {
        //        // Assuming you have a method in your service to save the cart item to the database
        //        await ShoppingCartService.SaveCartItemToDatabase(cartItemDto);

        //        // Optionally, you can refresh the list of cart items from the database here
        //        // ShoppingCartItems = await ShoppingCartService.GetCartItemsFromDatabase();
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle database save errors
        //        Console.WriteLine($"An error occurred while saving the cart item to the database: {ex.Message}");
        //    }
        //}
    }
}

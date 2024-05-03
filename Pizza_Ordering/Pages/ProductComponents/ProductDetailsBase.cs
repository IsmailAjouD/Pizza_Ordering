
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

        public string ErrorMessage { get; set; }
        public int SelectedSizeId { get; set; }


        public int Quantity { get; set; } = 1;
        public decimal TotalPrice { get; set; }
        public bool ShowText { get; set; } = false;

        public List<string> Descriptions { get; set; }

        protected List<bool> isCheckedList = new List<bool>();
        protected List<string> UncheckedDescriptions { get; set; }

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
               UncheckedDescriptions = new List<string>();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        public void GetUncheckeItems(int index)
        {
            if (!isCheckedList[index] && !UncheckedDescriptions.Contains(Descriptions[index]))
            {
                UncheckedDescriptions.Add(Descriptions[index]);
                Console.WriteLine("Unchecked Description: " + Descriptions[index]); // Output to console

                // Save the unchecked description to the database
            }
            else if (isCheckedList[index] && UncheckedDescriptions.Contains(Descriptions[index]))
            {
                UncheckedDescriptions.Remove(Descriptions[index]);
                Console.WriteLine("Unchecked Description Removed: " + Descriptions[index]); // Output to console
            }
        }

        //public void GetUncheckeItems(int index)
        //{
        //    // Check if Descriptions is not null and index is within its bounds
        //    if (Descriptions != null )
        //    {
        //        var description = Descriptions[index];

        //        // Ensure isCheckedList is initialized and has the correct count
        //        if (isCheckedList != null && isCheckedList.Count == Descriptions.Count)
        //        {
        //            if (!isCheckedList[index])
        //            {
        //                if (UncheckedDescriptions == null)
        //                {
        //                    UncheckedDescriptions = new List<string>();
        //                }

        //                if (!UncheckedDescriptions.Contains(description))
        //                {
        //                    UncheckedDescriptions.Add(description);
        //                    // Optionally, you can remove the item from the checked list
        //                     isCheckedList[index] = false;
        //                    Console.WriteLine("Unchecked Description Added: " + description); // Output to console
        //                }
        //            }
        //            else
        //            {
        //                if (UncheckedDescriptions != null && UncheckedDescriptions.Contains(description))
        //                {
        //                    UncheckedDescriptions.Remove(description);
        //                    Console.WriteLine("Unchecked Description Removed: " + description); // Output to console
        //                }
        //            }
        //        }
        //        else
        //        {
        //            // Handle the case where isCheckedList is not properly initialized
        //            Console.WriteLine("isCheckedList is not properly initialized");
        //        }
        //    }
        //    else
        //    {
        //        // Handle the case where Descriptions is null or index is out of bounds
        //        Console.WriteLine("Descriptions is null or index is out of bounds");
        //    }
        //}


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
                    Price = TotalPrice,
                    ExtraItemIds = string.Join(",", SelectedExtraItemIds),
                    RemovableItems = string.Join(" ",UncheckedDescriptions)

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

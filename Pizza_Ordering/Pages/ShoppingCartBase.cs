using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Microsoft.VisualBasic;
using Pizza_Ordering.web.Services.Contracts;
using Pizza_Ordering.web.Services.Cpntracts;
using PizzaOrdering.Models.Dtos;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.AccessControl;

namespace Pizza_Ordering.web.Pages
{
    public class ShoppingCartBase : ComponentBase
    {
        [Inject]
        public IJSRuntime Js { get; set; }

        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }

        [Inject]
        public IProductService ProductService { get; set; }
         public List<CartItemDto> ShoppingCartItems { get; set; }
        public ProductMenuDto Product { get; set; }
        public string ErrorMessage { get; set; }
       //public decimal TotalPrice { get; set; }
         protected int TotalQuantity { get; set; }
        //public CartItemDto CartItemDtoforQuantity { get; set; }
        //public int Quantity { get; set; }
        public int EstraitemId { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                ShoppingCartItems = await ShoppingCartService.GetCartItems(HardCoded.UserId);
                //ExtraItemDto = await ProductService.GetExtraItem(EstraitemId);
                //Product = await ProductService.GetProductsItems();
                //CalculetCatSummaryTotals();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task DeletRemovableitem(int cartItemid ,string Removable)
        {
            try
            {
                var cartItemDto = new CartIemDeletItemDto()
                {
                    CartItemId = cartItemid,
                    RemovableItems = Removable
                };
           
              var result=   await  ShoppingCartService.UpdatRemovableItem(cartItemDto);
                ShoppingCartItems = await ShoppingCartService.GetCartItems(HardCoded.UserId);

                if (result != null)
                {
                ShoppingCartItems = await ShoppingCartService.GetCartItems(HardCoded.UserId);
                StateHasChanged();

                }
                StateHasChanged();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task RemovExtraItem(int cartItemId,int exstraItemId)
        {
            try
            {
                var cartItem = new CartItemForDeletExtraItemDto()
                {
                    CartItemId= cartItemId,
                    ExtraItemId = exstraItemId,
                };
                 await ShoppingCartService.DeletExtraItem(cartItem);
                StateHasChanged();
            }
            catch (Exception)
            {

                throw;
            }
            //EstraitemId = Estraitemid;
            //Console.WriteLine(Estraitemid);
            ////var ConvetExtraitem = (pars int(32).Estraitemid);
            //var cartItem = ShoppingCartService.GetCartItembyId(id);
            //ExtraItemDto = await ProductService.GetExtraItem(Estraitemid);

        }

        public void Increment(CartItemDto cartItemDto)
        {
            cartItemDto.Qty++;
            //GetTotalPrice();
            UpdateQuantityCarItem_Click(cartItemDto.Id, cartItemDto.Qty,cartItemDto.SizeId);

            //GetPrice(cartItemDto);
            StateHasChanged();

        }
        public void Decrement(CartItemDto cartItemDto)
        {
            if (cartItemDto.Qty > 1)
            {
                cartItemDto.Qty--;
                //GetTotalPrice();
                UpdateQuantityCarItem_Click( cartItemDto.Id , cartItemDto.Qty,cartItemDto.SizeId);
                //GetPrice(cartItemDto);
                StateHasChanged();

            }
        }


        public async Task UpdateQuantityCarItem_Click(int id, int qty, int sizeId)
        {
            try
            {
                var cartItem = ShoppingCartItems.FirstOrDefault(i => i.Id == id);
                if (cartItem == null)
                {
                    Console.WriteLine("Cart item not found.");
                    return;
                }

                if (qty > 0)
                {
                    var newPrice = await CalculatePriceFromSizeIdAndQuantity(cartItem.ExtraItems, sizeId, qty);
                    if (cartItem.ExtraItems == null)
                    {
                        var updateItemDtonulExtraitem = new CartItemQtyUpdateDto
                        {
                            CartItemId = id,
                            Qty = qty,
                            Price = newPrice,
                    };
                        var returnedUpdateItemDto = await ShoppingCartService.UpdateQty(updateItemDtonulExtraitem);
                        if (returnedUpdateItemDto != null)
                        {
                            cartItem.Qty = returnedUpdateItemDto.Qty;
                            cartItem.Price = newPrice;
                            StateHasChanged();
                        }
                    }
                   if (cartItem.ExtraItems != null)
                    {

                        var updateItemDto = new CartItemQtyUpdateDto
                        {
                            CartItemId = id,
                            Qty = qty,
                            Price = newPrice,
                            ExtraItemsId = cartItem.ExtraItems?.Select(e => Convert.ToInt32(e.ExtraItemId)).ToList()
                        };

                        var returnedUpdateItemDto = await ShoppingCartService.UpdateQty(updateItemDto);
                        if (returnedUpdateItemDto !=null)
                        {
                            cartItem.Qty = returnedUpdateItemDto.Qty;
                            cartItem.Price = newPrice;
                            cartItem.ExtraItems = returnedUpdateItemDto.ExtraItems;
                            StateHasChanged();
                        }
                           StateHasChanged();
                    }
                }
                StateHasChanged();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating quantity: {ex.Message}");
                throw;
            }
        }

        //public ExtraItemDto ExtraItemDto { get; set; }

        //public int CalculatExtraitem()
        //{
        //    var carItem 
        //    var ExtraItem = new ExtraItemDto()
        //    {
        //        ExtraItemId
        //    };
        //}
        //private async Task<decimal> CalculatePriceFromSizeIdAndQuantity(List<ExtraItemDto> extraItems, int sizeId, int quantity)
        //{
        //    try
        //    {
        //        decimal totalPrice = 0;
        //        // Call your ProductService or any appropriate service method to get the price based on the size ID
        //        var size = await ProductService.GetProductbtSizeId(sizeId);
        //        if (size != null)
        //        {
        //            // Calculate the price based on the retrieved product size information
        //            if (extraItems == null)
        //            {

        //            totalPrice += size.Price * quantity;
        //            }
        //            // Add the price of each extra item to the total price
        //              //foreach (var extraItem in extraItems)
        //              //  {
        //              //      totalPrice += extraItem.Price;
        //              //  }
        //            if (extraItems != null && extraItems.Any())
        //            {
        //                foreach (var extraItem in extraItems)
        //                {
        //                    totalPrice += extraItem.Price;
        //                }
        //                totalPrice += size.Price * quantity;

        //            }
        //            else
        //            {
        //                totalPrice += size.Price * quantity;

        //            }

        //            return totalPrice;
        //        }
        //        else
        //        {
        //            // Handle the case where the size information is not found
        //            // For example, you can return a default value or throw an exception
        //            throw new Exception("Size information not found.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle any exceptions
        //        Console.WriteLine($"Error calculating price: {ex.Message}");
        //        throw;
        //    }
        //}
        private async Task<decimal> CalculatePriceFromSizeIdAndQuantity(List<ExtraItemDto> extraItems, int sizeId, int quantity)
        {
            try
            {
                decimal totalPrice = 0;

                // Get the price of the product based on the provided size ID
                var size = await ProductService.GetProductbtSizeId(sizeId);
                if (size != null)
                {
                    // Calculate the total price based on product price and quantity
                    totalPrice += size.Price * quantity;

                    // Add the price of each extra item to the total price
                    if (extraItems != null && extraItems.Any())
                    {
                        foreach (var extraItem in extraItems)
                        {
                            totalPrice += extraItem.Price; StateHasChanged();
                        }
                    }

                    return totalPrice;
                    StateHasChanged();
                }
                else
                {
                    // Handle the case where the size information is not found
                    // For example, you can return a default value or throw an exception
                    throw new Exception("Size information not found.");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                Console.WriteLine($"Error calculating price: {ex.Message}");
                throw;
            }
        }

        private void CalculetCatSummaryTotals()
        {
            //SetTotalPrice();
            SetTotalQuantity();
        }
        //private void SetTotalPrice()
        //{
        //    TotalPrice = ShoppingCartItems.Sum(p => p.Price).ToString("C");
        //}
        private void SetTotalQuantity()
        {
            TotalQuantity = ShoppingCartItems.Sum(p => p.Qty);
        }
        private async Task UpdateItemPrice(CartItemDto cartItemDto)
        {
            var item = GetCartItem(cartItemDto.Id);
 

            //await ManageCartItemsLocalStorageService.SaveCollection(ShoppingCartItems);

        }
        protected decimal PriceafterchangQty { get; set; }
        public void GetPrice(CartItemDto cartItemDto)
        {
            var pruductSize = Product.Sizes.FirstOrDefault(s => s.SizeId ==cartItemDto.SizeId);
           if (pruductSize != null)
            {
                PriceafterchangQty = pruductSize.Price * cartItemDto.Qty;

            }
            //var Price = selectedSize != null ? (selectedSize.Price * Quantity) : 0;

            //foreach (var itemId in SelectedExtraItemIds)
            //{
            //    var extraItem = ExtraItems.FirstOrDefault(item => item.ExtraItemId == itemId);
            //    if (extraItem != null)
            //    {
            //        totalPrice += extraItem.Price;
            //    }
            //}

            //TotalPrice = totalPrice;
            StateHasChanged();
        }
        protected async Task DeleteCartItem_Click(int id)
        {
            var cartItemDto = await ShoppingCartService.DeleteItem(id);
            RemoveCArtItem(cartItemDto.Id);
            CalculetCatSummaryTotals();
        }
        private void RemoveCArtItem(int id)
        {
            var cartItemDto = GetCartItem(id);
            ShoppingCartItems.Remove(cartItemDto);
        }
        private CartItemDto GetCartItem(int id)
        {
            return ShoppingCartItems.FirstOrDefault(i => i.Id == id);
           
        }

      

    }
}

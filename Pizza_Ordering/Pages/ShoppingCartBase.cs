using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Pizza_Ordering.web.Services.Contracts;
using Pizza_Ordering.web.Services.Cpntracts;
using PizzaOrdering.Models.Dtos;
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
        public string ErrorMessage { get; set; }
        public string TotalPrice { get; set; }
         protected int TotalQuantity { get; set; }
        public CartItemDto CartItemDtoforQuantity { get; set; }
        //public int Quantity { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                ShoppingCartItems = await ShoppingCartService.GetCartItems(HardCoded.UserId);
             
                //CalculetCatSummaryTotals();
            }
            catch (Exception)
            {

                throw;
            }
        }



        public void Increment(CartItemDto cartItemDto)
        {
            cartItemDto.Qty++;
            //GetTotalPrice();
            UpdateQuantityCarItem_Click(cartItemDto.Id, cartItemDto.Qty);
        }
        public void Decrement(CartItemDto cartItemDto)
        {
            if (cartItemDto.Qty > 1)
            {
                cartItemDto.Qty--;
                //GetTotalPrice();
                UpdateQuantityCarItem_Click( cartItemDto.Id , cartItemDto.Qty);
            }
        }

        private void CalculetCatSummaryTotals()
        {
            SetTotalPrice();
            SetTotalQuantity();
        }
        private void SetTotalPrice()
        {
            TotalPrice = ShoppingCartItems.Sum(p => p.Price).ToString("C");
        }
        private void SetTotalQuantity()
        {
            TotalQuantity = ShoppingCartItems.Sum(p => p.Qty);
        }
        public async Task UpdateQuantityCarItem_Click(int id , int qty)
        {
            try
            {
                if (qty > 0)
                {
                    var updateItemDto = new CartItemQtyUpdateDto
                    {
                        CartItemId = id,
                        Qty = qty,              
                    };
                    var returnedUpdateItemDto = await ShoppingCartService.UpdateQty(updateItemDto);
                    //CalculetCatSummaryTotals();
                    //await UpdateItemTotalPrice(returnedUpdateItemDto);
                    //CartChanged();
                }
                else
                {
                    var item = this.ShoppingCartItems.FirstOrDefault(i => i.Id == id);

                    if (item != null)
                    {
                        item.Qty = 1;
                        item.TotalPrice = item.Price;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private async Task UpdateItemTotalPrice(CartItemDto cartItemDto)
        {
            var item = GetCartItem(cartItemDto.Id);
            //var product = ProductService.GetProductsItem(item.Id);
            //if(product != null)
            //{
            //    product
            //}
            if (item != null)
            {
                item.TotalPrice = cartItemDto.Price * cartItemDto.Qty;
            }

            //await ManageCartItemsLocalStorageService.SaveCollection(ShoppingCartItems);

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

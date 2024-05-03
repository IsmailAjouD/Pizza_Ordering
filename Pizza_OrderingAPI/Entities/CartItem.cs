using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components.Forms;

namespace Pizza_OrderingAPI.Entities
{
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int SizeId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string RemovableItems {  get; set; }
        public string? ExtraItemsId { get; set; }   

    }
}

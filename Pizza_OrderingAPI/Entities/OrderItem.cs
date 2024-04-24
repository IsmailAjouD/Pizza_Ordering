using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace Pizza_OrderingAPI.Entities
{
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        public int PizzaId { get; set; }
        [ForeignKey("PizzaId")]
        public ProductMenu Pizza { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal { get; set; }
    }
}

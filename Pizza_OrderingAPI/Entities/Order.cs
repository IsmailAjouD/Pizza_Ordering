using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Pizza_OrderingAPI.Entities
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public User Customer { get; set; }
        public DateTime OrderDate { get; set; }
        public string DeliveryAddress { get; set; }
        public decimal TotalAmount { get; set; }
        public int StatusId { get; set; }
        [ForeignKey("StatusId")]
        public OrderStatus Status { get; set; }
        public DateTime? DesiredDeliveryTime { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Pizza_OrderingAPI.Entities
{
    public class OrderConfirmation
    {
        [Key]
        public int ConfirmationId { get; set; }
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        public DateTime ConfirmationDate { get; set; }
        public string ConfirmationMessage { get; set; }
    }
}

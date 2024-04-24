using System.ComponentModel.DataAnnotations;

namespace Pizza_OrderingAPI.Entities
{
    public class OrderStatus
    {
        [Key]
        public int StatusId { get; set; }
        public string StatusName { get; set; }
    }
}

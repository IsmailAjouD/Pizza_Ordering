using PizzaOrdering.Models.Dtos;

namespace Pizza_OrderingAPI.Entities
{
    public class ExtraItem
    {
        public int ExtraItemId { get; set; } 
        public string Name { get; set; }

        public decimal Price { get; set; }

    }
}

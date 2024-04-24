using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pizza_OrderingAPI.Entities
{
    public class ProductMenu
    {
        [Key]
        public int PizzaId { get; set; }
        public string PizzaName { get; set; }
        public int PizzaNum { get; set; }
    
        public string Description { get; set; }
        public string? ImageURL { get; set; }
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public ItemCategory ItemCategory { get; set; }

        public ICollection<ProductSize> Sizes { get; set; }

    }
}

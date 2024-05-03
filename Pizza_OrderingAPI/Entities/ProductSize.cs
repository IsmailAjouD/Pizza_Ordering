using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pizza_OrderingAPI.Entities
{
    public class ProductSize
    {
        [Key]
        public int SizeId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        //Foreign key property
        public int PizzaId { get; set; }

        // Navigation property
        [ForeignKey("PizzaId")]
        public ProductMenu Pizza { get; set; }


    }
}

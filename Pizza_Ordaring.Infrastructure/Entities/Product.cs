using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Pizza_Ordaring.Infrastructure.Entities
{
    public class Product
    {
        [Key]
        public int PizzaId { get; set; }
        public string PizzaName { get; set; }
        public int PizzaNum { get; set; }

        public string Description { get; set; }
        public string? ImageURL { get; set; }

        [Column("CategoryId")]
        public int CategoryId { get; set; }

    }
}

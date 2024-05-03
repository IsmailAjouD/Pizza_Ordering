using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Pizza_Ordaring.Infrastructure.Entities
{
    public class ProductSize
    {
        [Key]
        public int SizeId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }


    }
}

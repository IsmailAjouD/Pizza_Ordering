using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrdering.Models.Dtos
{
    public class ProductMenuDto
    {
        public int PizzaId { get; set; }
        public int PizzaNum { get; set; }
        public string PizzaName { get; set; }
        public string Description { get; set; }
        public string? ImageURL { get; set; }
 
        public int? CategoryId { get; set; }
        public ICollection<ProductSizeDto> Sizes { get; set; }
        public string CategoryName { get; set; }
    }
}

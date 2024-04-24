using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrdering.Models.Dtos
{
    public class ProductSizeDto
    {
        public int SizeId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        //public int Quantity { get; set; }
    }
}

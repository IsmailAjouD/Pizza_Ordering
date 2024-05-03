using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrdering.Models.Dtos
{
    public  class CartItemToAddDto
    {
        public int ProductId { get; set; }
        public int CartId { get; set; }
        public int Quntity { get; set; }
        public int ProductSize { get; set; }
        public decimal Price {  get; set; }
        public string ExtraItemIds { get; set; }
        public string RemovableItems { get; set; }
    }
}

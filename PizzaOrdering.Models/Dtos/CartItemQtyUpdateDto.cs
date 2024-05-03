using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrdering.Models.Dtos
{
    public class CartItemQtyUpdateDto
    {
        public int CartItemId { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public List<int> ExtraItemsId { get; set; } = new List<int>(); // List of ExtraItem IDs, initialized as empty list

    }
}

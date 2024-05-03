using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrdering.Models.Dtos
{
    public class CartItemDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CartId { get; set; }
        public string ProductName { get; set; }
        public string PizzaSize { get; set; }
        public string ProductDescription { get; set; }
        public string ProductImageURL { get; set; }
        //public decimal Price { get; set; }
        public decimal Price { get; set; }
        public int Qty { get; set; }
        public int SizeId { get; set; }
        public string RemovableItems { get; set; }

        public List<ExtraItemDto>? ExtraItems { get; set; }


    }
}

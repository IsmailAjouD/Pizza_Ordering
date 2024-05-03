using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordaring.Shared.Models
{
    public class ProductSizeModel
    {
        [Key]
        public int SizeId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }


    }
}

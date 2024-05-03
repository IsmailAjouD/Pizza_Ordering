using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordaring.Shared.Models
{
    public class ProductModel
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


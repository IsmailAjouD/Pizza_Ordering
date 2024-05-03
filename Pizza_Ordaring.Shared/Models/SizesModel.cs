using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordaring.Shared.Models
{
    public class SizesModel
    {
        public int Id { get; set; }
        [Column("PizzaId")]
        public int PizzaId { get; set; }
        [Column("SizeId")]
        public int SizeId { get; set; }
    }
}

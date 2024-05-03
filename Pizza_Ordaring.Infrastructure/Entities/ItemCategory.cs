
using System.ComponentModel.DataAnnotations;

namespace Pizza_Ordaring.Infrastructure.Entities
{
    public class ItemCategory
    {
        [Key]
        public int Id { get; set; }
        public string CategoryName { get; set; }
    }
}

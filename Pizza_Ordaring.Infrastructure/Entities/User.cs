using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Pizza_Ordaring.Infrastructure.Entities
{
    public class User
    {
        [Key]
        public int CustomerId { get; set; }
        public string Name { get; set; }
        //public string Email { get; set; }
        //public string PhoneNumber { get; set; }
        //public string Address { get; set; }
        //public string CityCode { get; set; }
        //[ForeignKey("CityCode")]
        //public City City { get; set; }

    }
}

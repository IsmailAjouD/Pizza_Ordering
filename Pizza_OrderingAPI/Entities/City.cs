using System.ComponentModel.DataAnnotations;

namespace Pizza_OrderingAPI.Entities
{
    public class City
    {
        [Key]
        public string CityCode { get; set; }
        public string CityName { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace Dashboard.Model
{
    public class Location
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ZipCode { get; set; }
        public string CityName { get; set; }
        public string Country { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}

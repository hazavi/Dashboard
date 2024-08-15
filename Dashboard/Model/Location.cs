using System.ComponentModel.DataAnnotations.Schema;

namespace Dashboard.Model
{
    public class Location
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string City { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}

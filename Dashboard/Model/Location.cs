using System.ComponentModel.DataAnnotations.Schema;

namespace Dashboard.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Location
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        [Required]
        public int ZipCode { get; set; }

        [Required]
        public string CityName { get; set; }

        [Required]
        public string Country { get; set; }

        // Foreign key to User
        [ForeignKey("UserId")]
        public User User { get; set; }
    }

}

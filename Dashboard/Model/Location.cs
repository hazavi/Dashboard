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

        [Required(ErrorMessage = "Enter Zip Code")]
        public int ZipCode { get; set; }

        [Required(ErrorMessage = "Enter City Name")]
        [StringLength(100)]
        public string CityName { get; set; }

        [Required(ErrorMessage = "Enter Country")]
        [StringLength(100)]
        public string Country { get; set; }

        // Foreign key to User
        [ForeignKey("UserId")]
        public User User { get; set; }
    }

}

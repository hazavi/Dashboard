using System.ComponentModel.DataAnnotations;

namespace Dashboard.Model
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please provide Username")]
        [StringLength(20)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please provide a Password")]
        [StringLength(20)]
        public string Password { get; set; }


        [Required(ErrorMessage = "Please provide an Email")]
        [StringLength(100)]
        public string Email { get; set; }

        public Location Location { get; set; }

    }
}

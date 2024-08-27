using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dashboard.Model
{
    public class TodoList
    {
        public int Id { get; set; }
        public int UserId { get; set; } // Foreign key that references the User who owns this TodoList
        public DateOnly TodoDue { get; set; } = DateOnly.FromDateTime(DateTime.Now); //sets the datetime to now

        [Required(ErrorMessage = "Please provide a Title")]
        [StringLength(20)]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public bool isDone { get; set; }

        public string? Color { get; set; } = "#ffd700"; // sets a default color
        public bool IsPinned { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }


    }
}

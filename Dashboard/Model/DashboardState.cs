namespace Dashboard.Model
{
    public class DashboardState
    {
        public int UserId { get; set; }
        public bool ShowWeather { get; set; }
        public bool ShowNews { get; set; }
        public bool ShowTodo { get; set; }
        public bool ShowCalendar { get; set; }

        public User User { get; set; } // Navigation property

    }

}

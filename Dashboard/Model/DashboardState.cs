namespace Dashboard.Model
{
    public class DashboardState
    {
        //user that owns this dashboard state
        public int UserId { get; set; }
        public bool ShowWeather { get; set; }
        public bool ShowNews { get; set; }
        public bool ShowTodo { get; set; }
        public bool ShowCalendar { get; set; }
        public bool ShowMovies { get; set; }

        public User User { get; set; } 

    }

}

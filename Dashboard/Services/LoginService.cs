namespace Dashboard.Services
{
    public class LoginService
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public bool IsLoggedIn { get; set; }
        public bool IsAdmin { get; set; }
    }
}

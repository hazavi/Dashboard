using Dashboard.Model.Users;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace Dashboard.Connect
{
    public class DashboardDbContext : DbContext
    {
        public DashboardDbContext(DbContextOptions<DashboardDbContext> options)
            : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
    }
}

using Dashboard.Model;
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
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<TodoList> TodoLists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure User-Setting relationship
            modelBuilder.Entity<Setting>()
                .HasOne<User>() // Navigation property not needed if not used
                .WithMany() // No navigation property on User side
                .HasForeignKey(s => s.UserId);

            // Configure User-Location relationship
            modelBuilder.Entity<Location>()
                .HasOne<User>() // Navigation property not needed if not used
                .WithMany() // No navigation property on User side
                .HasForeignKey(l => l.UserId);

            // Configure default value for DateCreated in TodoList
            modelBuilder.Entity<TodoList>()
                .Property(t => t.DateCreated)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}

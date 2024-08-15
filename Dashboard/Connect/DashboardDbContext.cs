using Dashboard.Model;
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
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<TodoList> TodoLists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //sætter userId som foreign key
            modelBuilder.Entity<Setting>()
                .HasOne(s => s.User)
                .WithMany()
                .HasForeignKey(s => s.UserId);
            
            modelBuilder.Entity<Location>()
                .HasOne(s => s.User)
                .WithMany()
                .HasForeignKey(s => s.UserId);

            //automatisk sætter en datecreate når man laver en todo
            modelBuilder.Entity<TodoList>()
                .Property(e => e.DateCreated)
                .HasDefaultValueSql("GETDATE()");
        }

    }
}

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
        public DbSet<DashboardState> DashboardStates { get; set; }
        public async Task<DashboardState> GetDashboardStateAsync(int userId)
        {
            return await DashboardStates.FirstOrDefaultAsync(ds => ds.UserId == userId);
        }

        public async Task SaveDashboardStateAsync(DashboardState state)
        {
            try
            {
                if (state.UserId == 0)
                {
                    throw new ArgumentException("UserId cannot be zero.");
                }

                var existingState = await DashboardStates
                    .FirstOrDefaultAsync(ds => ds.UserId == state.UserId);

                if (existingState != null)
                {
                    // Update existing state
                    existingState.ShowWeather = state.ShowWeather;
                    existingState.ShowNews = state.ShowNews;
                    existingState.ShowTodo = state.ShowTodo;
                    existingState.ShowCalendar = state.ShowCalendar;
                    existingState.ShowMovies = state.ShowMovies;
                    DashboardStates.Update(existingState);
                }
                else
                {
                    // Add new state
                    DashboardStates.Add(state);
                }

                await SaveChangesAsync();
                Console.WriteLine("Dashboard state saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving dashboard state: {ex.Message}");
            }
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure User-Setting relationship
            modelBuilder.Entity<Setting>()
                .HasOne<User>() // Navigation property not needed if not used
                .WithMany() // No navigation property on User side
                .HasForeignKey(s => s.UserId);

            // Configure User-Location relationship
            modelBuilder.Entity<User>()
                .HasOne(u => u.Location)
                .WithOne(l => l.User)
                .HasForeignKey<Location>(l => l.UserId);

            modelBuilder.Entity<TodoList>()
                .Property(t => t.DateCreated)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<DashboardState>()
        .HasKey(ds => ds.UserId); // Define UserId as the primary key

            modelBuilder.Entity<DashboardState>()
            .HasOne(ds => ds.User)
            .WithMany(u => u.DashboardStates)
            .HasForeignKey(ds => ds.UserId)
            .OnDelete(DeleteBehavior.Restrict); // Adjust if needed

            modelBuilder.Entity<User>()
                .HasKey(u => u.Id); // Ensure the primary key is set
        }
    }
}

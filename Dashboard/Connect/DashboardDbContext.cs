using Dashboard.Model;
using Microsoft.EntityFrameworkCore;

namespace Dashboard.Connect
{
    //DbContext to connect dashboard app and sql database using EntityFrameworkCore
    public class DashboardDbContext : DbContext
    {
        public DashboardDbContext(DbContextOptions<DashboardDbContext> options)
            : base(options)
        {
        }

        // DbSets represent tables in the database
        public DbSet<User> Users { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<TodoList> TodoLists { get; set; }
        public DbSet<DashboardState> DashboardStates { get; set; }

        // Method to get a user's DashboardState 
        public async Task<DashboardState> GetDashboardStateAsync(int userId)
        {
            // searches the DashboardStates table for the specific user's dashboard state
            return await DashboardStates.FirstOrDefaultAsync(ds => ds.UserId == userId);
        }

        // Method to save or update a user's DashboardState 
        public async Task SaveDashboardStateAsync(DashboardState state)
        {
            try
            {
                if (state.UserId == 0)
                {
                    throw new ArgumentException("UserId cannot be zero.");
                }

                // Check if the dashboard state already exists for the user
                var existingState = await DashboardStates
                    .FirstOrDefaultAsync(ds => ds.UserId == state.UserId);

                if (existingState != null)
                {
                    // If the state exists, update it with the new values
                    existingState.ShowWeather = state.ShowWeather;
                    existingState.ShowNews = state.ShowNews;
                    existingState.ShowTodo = state.ShowTodo;
                    existingState.ShowCalendar = state.ShowCalendar;
                    existingState.ShowMovies = state.ShowMovies;
                    DashboardStates.Update(existingState);
                }
                else
                {
                    // If the state doesn't exist, add a new one
                    DashboardStates.Add(state);
                }

                // Save the changes to the database
                await SaveChangesAsync();
                Console.WriteLine("Dashboard state saved successfully.");
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during the save operation
                Console.WriteLine($"Error saving dashboard state: {ex.Message}");
            }
        }

        // Configures the entity relationships and properties using the ModelBuilder
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the relationship between Setting and User
            modelBuilder.Entity<Setting>()
                .HasOne<User>() // a Setting has one related User
                .WithMany() // a User can have many related Settings (though no navigation property is specified)
                .HasForeignKey(s => s.UserId); // Specifies the foreign key in Setting

            // Configure the one-to-one relationship between User and Location
            modelBuilder.Entity<User>()
                .HasOne(u => u.Location) // A User has one Location
                .WithOne(l => l.User) // A Location has one User
                .HasForeignKey<Location>(l => l.UserId); // The foreign key is in the Location entity

            // sets DateTime on DateCreate automatically on the time it was created
            modelBuilder.Entity<TodoList>()
                .Property(t => t.DateCreated)
                .HasDefaultValueSql("GETDATE()"); // Uses the SQL GETDATE() function to set the default

            // Set UserId as the primary key for DashboardState
            modelBuilder.Entity<DashboardState>()
                .HasKey(ds => ds.UserId); // Defines UserId as the primary key

            // Configure the relationship between DashboardState and User
            modelBuilder.Entity<DashboardState>()
                .HasOne(ds => ds.User) // A DashboardState belongs to one User
                .WithMany(u => u.DashboardStates) // A User can have many DashboardStates
                .HasForeignKey(ds => ds.UserId) // Specifies the foreign key in DashboardState
                .OnDelete(DeleteBehavior.Restrict); // Restricts delete action to prevent cascade deletion

            // Ensure that the primary key for User is set
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id); // Defines Id as the primary key for User
        }
    }
}

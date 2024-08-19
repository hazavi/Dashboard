using Microsoft.EntityFrameworkCore;
using WeatherAPI.Models;

namespace WeatherAPI
{
    public class WeatherDbContext : DbContext
    {
        public WeatherDbContext(DbContextOptions<WeatherDbContext> options)
            : base(options)
        {
        }

        public DbSet<WeatherData> WeatherData { get; set; }
    }
}

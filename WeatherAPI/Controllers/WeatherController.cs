using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using WeatherAPI.Models;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using WeatherAPI.Models;
using WeatherAPI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeatherAPI.Services;

namespace WeatherAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly WeatherService _weatherService;
        private readonly WeatherDbContext _context;

        public WeatherController(WeatherService weatherService, WeatherDbContext context)
        {
            _weatherService = weatherService;
            _context = context;
        }

        [HttpGet("{city}")]
        public async Task<IActionResult> GetWeather(string city)
        {
            var weatherResponse = await _weatherService.GetWeatherAsync(city);

            if (weatherResponse == null)
            {
                return NotFound("Weather data not found.");
            }

            // Save weather data to the database
            var weather = new WeatherData
            {
                City = weatherResponse.Name,
                Temperature = weatherResponse.Main.Temp,
                Timestamp = DateTime.UtcNow
            };

            _context.WeatherData.Add(weather);
            await _context.SaveChangesAsync();

            return Ok(weatherResponse);
        }
    }

}

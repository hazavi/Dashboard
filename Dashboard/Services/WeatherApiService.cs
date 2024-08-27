using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class WeatherApiService
{
    private readonly HttpClient _httpClient;

    // Constructor to inject HttpClient
    public WeatherApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Fetches weather data for a given city
    public async Task<WeatherData> GetWeatherAsync(string city)
    {
        // Make API request and deserialize response
        var response = await _httpClient.GetFromJsonAsync<OpenWeatherResponse>(
            $"weather?q={city}&appid=96480201313603b6e3bf519e7d1e5c10&units=metric"
        );

        if (response != null)
        {
            // Map response to WeatherData object
            return new WeatherData
            {
                City = response.Name,
                Temperature = response.Main.Temp,
                WeatherMain = response.Weather[0].Main,
                WeatherDescription = response.Weather[0].Description,
                Humidity = response.Main.Humidity,
                WindSpeed = response.Wind.Speed,
                IconUrl = $"http://openweathermap.org/img/wn/{response.Weather[0].Icon}@2x.png",
                Timestamp = DateTime.UtcNow
            };
        }
        return null; // Return null if no response
    }
}

// Class representing the structure of the API response
public class OpenWeatherResponse
{
    public string Name { get; set; }
    public MainData Main { get; set; }
    public WeatherData[] Weather { get; set; }
    public WindData Wind { get; set; }

    // Nested class for main weather data (temperature and humidity)
    public class MainData
    {
        public float Temp { get; set; }
        public int Humidity { get; set; }
    }

    // Nested class for weather conditions
    public class WeatherData
    {
        public string Main { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }

    // Nested class for wind data
    public class WindData
    {
        public float Speed { get; set; }
    }
}

// Class for storing formatted weather data
public class WeatherData
{
    public string City { get; set; }
    public float Temperature { get; set; }
    public string WeatherMain { get; set; }
    public string WeatherDescription { get; set; }
    public int Humidity { get; set; }
    public float WindSpeed { get; set; }
    public string IconUrl { get; set; }

    // Property to get rounded temperature (to the nearest integer)
    public int RoundedTemperature => (int)Math.Round(Temperature);

    public DateTime Timestamp { get; set; }
}

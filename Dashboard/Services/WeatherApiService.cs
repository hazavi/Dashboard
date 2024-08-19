using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class WeatherApiService
{
    private readonly HttpClient _httpClient;

    public WeatherApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<WeatherData> GetWeatherAsync(string city)
    {
        var response = await _httpClient.GetFromJsonAsync<OpenWeatherResponse>($"weather?q={city}&appid=96480201313603b6e3bf519e7d1e5c10&units=metric");
        if (response != null)
        {
            return new WeatherData
            {
                City = response.Name,
                Temperature = response.Main.Temp,
                WeatherMain = response.Weather[0].Main,
                WeatherDescription = response.Weather[0].Description,
                Humidity = response.Main.Humidity,
                WindSpeed = response.Wind.Speed,
                Timestamp = DateTime.UtcNow // Set timestamp to current UTC time
            };
        }
        return null;
    }
}

// Update to match OpenWeatherMap's response structure
public class OpenWeatherResponse
{
    public string Name { get; set; }
    public MainData Main { get; set; }
    public WeatherData[] Weather { get; set; }
    public WindData Wind { get; set; }

    public class MainData
    {
        public float Temp { get; set; }
        public int Humidity { get; set; }
    }

    public class WeatherData
    {
        public string Main { get; set; }
        public string Description { get; set; }
    }

    public class WindData
    {
        public float Speed { get; set; }
    }
}

public class WeatherData
{
    public string City { get; set; }
    public float Temperature { get; set; }
    public string WeatherMain { get; set; }
    public string WeatherDescription { get; set; }
    public int Humidity { get; set; }
    public float WindSpeed { get; set; }

    // Property to get rounded temperature
    public int RoundedTemperature => (int)Math.Round(Temperature);

    public DateTime Timestamp { get; set; }
}

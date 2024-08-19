using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace WeatherAPI.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "96480201313603b6e3bf519e7d1e5c10"; // Replace with your actual API key
        private const string BaseUrl = "https://api.openweathermap.org/data/2.5/weather";

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WeatherResponse> GetWeatherAsync(string city)
        {
            var url = $"{BaseUrl}?q={city}&appid={ApiKey}"; 
            var response = await _httpClient.GetFromJsonAsync<WeatherResponse>(url);
            return response;
        }
    }

    public class WeatherResponse
    {
        public Main Main { get; set; }
        public string Name { get; set; }
    }

    public class Main
    {
        public float Temp { get; set; }
    }
}

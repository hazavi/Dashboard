using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dashboard.Services
{
    public class CalendarService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "DjjGk3gO3t7R5ZOaP3StJKzr7ZZJMEZp"; // Replace with your API key
        private readonly string _baseApiUrl = "https://calendarific.com/api/v2/holidays";

        public CalendarService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Holiday>> GetHolidaysAsync()
        {
            var year = DateTime.Now.Year;
            var url = $"{_baseApiUrl}?&api_key={_apiKey}&country=US&year={year}";

            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"API Response: {content}");

                var result = JsonConvert.DeserializeObject<CalendarificResponse>(content);
                return result?.Response?.Holidays ?? new List<Holiday>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new List<Holiday>();
            }
        }
    }

    public class CalendarificResponse
    {
        [JsonProperty("response")]
        public Response Response { get; set; }
    }

    public class Response
    {
        [JsonProperty("holidays")]
        public List<Holiday> Holidays { get; set; }
    }

    public class Holiday
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Country Country { get; set; }
        public Date Date { get; set; }
        public List<string> Type { get; set; }
        public string PrimaryType { get; set; }
        public string CanonicalUrl { get; set; }
        public string UrlId { get; set; }
        public string Locations { get; set; }
        public string States { get; set; }
    }

    public class Country
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class Date
    {
        public string Iso { get; set; }
        public DateTimeOffset DateTime { get; set; }
    }
}

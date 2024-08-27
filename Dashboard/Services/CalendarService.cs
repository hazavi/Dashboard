using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dashboard.Services
{
    // Service class for interacting with the Calendarific API to retrieve holiday data
    public class CalendarService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "DjjGk3gO3t7R5ZOaP3StJKzr7ZZJMEZp"; // API key for authentication
        private readonly string _baseApiUrl = "https://calendarific.com/api/v2/holidays"; // Base URL for the API

        // Constructor to initialize HttpClient
        public CalendarService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Fetches holiday data for the current year from the API
        public async Task<List<Holiday>> GetHolidaysAsync()
        {
            var year = DateTime.Now.Year; // Get the current year
            var url = $"{_baseApiUrl}?&api_key={_apiKey}&country=US&year={year}"; // Construct the API URL

            try
            {
                var response = await _httpClient.GetAsync(url); // Send the request to the API
                response.EnsureSuccessStatusCode(); // Ensure the request was successful

                var content = await response.Content.ReadAsStringAsync(); // Read the response content
                Console.WriteLine($"API Response: {content}"); // Log the response for debugging

                var result = JsonConvert.DeserializeObject<CalendarificResponse>(content); // Deserialize the JSON response(JSON string to object)
                return result?.Response?.Holidays ?? new List<Holiday>(); // Return the list of holidays or an empty list
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}"); // Log any exceptions
                return new List<Holiday>(); // Return an empty list in case of an error
            }
        }
    }

    // Classes for API response
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

using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace Dashboard.Services
{
    public class NewsService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public NewsService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = "371fe796d89e4bcd96baa20d8be74122";
        }

        public async Task<NewsResponse> GetTopNewsAsync()
        {
            var url = $"https://api.worldnewsapi.com/top-news?source-country=us&language=en&headlines-only=false&date=2024-08-21&api-key={_apiKey}";

            try
            {
                var response = await _httpClient.GetStringAsync(url);
                Console.WriteLine(response); // Log response for debugging

                var newsResponse = JsonConvert.DeserializeObject<NewsResponse>(response);
                return newsResponse;
            }
            catch (HttpRequestException e)
            {
                throw new Exception($"Request error: {e.Message}");
            }
        }


    }

    public class NewsResponse
    {
        [JsonProperty("top_news")]
        public TopNews[] TopNews { get; set; }
    }

    public class TopNews
    {
        [JsonProperty("news")]
        public NewsItem[] News { get; set; }
    }

    public class NewsItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Summary { get; set; }
        public string Url { get; set; }
        public string Image { get; set; }
        public string PublishDate { get; set; }
        public string Author { get; set; }

        public string ImageUrl => !string.IsNullOrEmpty(Image) ? Image : string.Empty;
    }
}

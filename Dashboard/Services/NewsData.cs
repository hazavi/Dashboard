using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Dashboard.Services
{
    public class NewsDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public NewsDataService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = "pub_5139706630665ef913c59b36433a793f18509";
        }

        public async Task<NewsDataResponse> GetTopNewsAsync()
        {
            var url = $"https://newsdata.io/api/1/latest?apikey={_apiKey}&language=en&category=world&timezone=Europe/Berlin&removeduplicate=1&size=10&prioritydomain=top&domain=bbc";

            try
            {
                var response = await _httpClient.GetStringAsync(url);
                Console.WriteLine(response); // Log response for debugging

                var newsResponse = JsonConvert.DeserializeObject<NewsDataResponse>(response);
                return newsResponse;
            }
            catch (HttpRequestException e)
            {
                throw new Exception($"Request error: {e.Message}");
            }
        }
    }

    // Response model
    public class NewsDataResponse
    {
        public string Status { get; set; }
        public int TotalResults { get; set; }

        [JsonProperty("results")]
        public NewsDataItem[] Results { get; set; }
    }

    // News item model
    public class NewsDataItem
    {
        public string ArticleId { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string PubDate { get; set; } // API returns as string

        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }
        public string SourceId { get; set; }
        public string SourceName { get; set; }
        public string SourceUrl { get; set; }
        public string Language { get; set; }
        public string[] Country { get; set; }
        public string[] Category { get; set; }
        public bool Duplicate { get; set; }

        // Property for image URL
        public string ImageUrlToShow => !string.IsNullOrEmpty(ImageUrl) ? ImageUrl : string.Empty;
    }

}

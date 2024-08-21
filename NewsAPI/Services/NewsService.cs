using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using NewsAPI.Model;

namespace NewsAPI.Services
{
    public class NewsService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public NewsService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _apiKey = configuration["NewsApiKey"] ?? throw new ArgumentNullException("NewsApiKey");
        }

        public async Task<NewsResponse> GetTopNewsAsync()
        {
            var url = $"https://api.worldnewsapi.com/top-news?source-country=us&language=en&headlines-only=false&api-key={_apiKey}";

            try
            {
                var response = await _httpClient.GetStringAsync(url);
                var newsResponse = JsonSerializer.Deserialize<NewsResponse>(response, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true // Allows case-insensitive property names
                });

                if (newsResponse == null)
                {
                    throw new Exception("Failed to deserialize news response.");
                }

                return newsResponse;
            }
            catch (HttpRequestException e)
            {
                // Log or handle HTTP request errors
                throw new Exception($"Request error: {e.Message}", e);
            }
            catch (JsonException e)
            {
                // Log or handle JSON deserialization errors
                throw new Exception($"Deserialization error: {e.Message}", e);
            }
            catch (Exception e)
            {
                // Handle any other errors
                throw new Exception($"An error occurred: {e.Message}", e);
            }
        }
    }
}

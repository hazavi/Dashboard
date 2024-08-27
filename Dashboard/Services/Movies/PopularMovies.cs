using RestSharp;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dashboard.Services.Movies
{
    public class PopularMovies
    {
        private readonly RestClient _client; // RestClient instance for making HTTP requests
        private const string BearerToken = "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiIwMTExYTliOTgzNmIyM2JkN2NlNWVjMjNhNDMyYWM0NCIsIm5iZiI6MTcyNDczNzYwNS4wODM2MTIsInN1YiI6IjY2Y2MyYWQ2ZDZjOWM0MzliMzFkNzI1MCIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.zblG2STINV80mvXBwijgedtIRy_Zst8UjN14gi1dEFg"; // API key for authorization

        // Constructor to initialize(set up) the RestClient with the base URL
        public PopularMovies()
        {
            var options = new RestClientOptions("https://api.themoviedb.org/3/");
            _client = new RestClient(options);
        }

        // Asynchronously fetches popular movies from the API
        public async Task<List<PopularMovie>> GetPopularMoviesAsync()
        {
            var request = new RestRequest("movie/popular", Method.Get); // Create a request for the "movie/popular" endpoint
            request.AddParameter("language", "en-US"); // Add parameters to the request
            request.AddParameter("page", "1");
            request.AddHeader("accept", "application/json"); // Set request headers
            request.AddHeader("Authorization", $"Bearer {BearerToken}");

            var response = await _client.ExecuteAsync(request); // Send the request asynchronously and get the response

            if (response.IsSuccessful)
            {
                Console.WriteLine("Response Content: " + response.Content); // Log the response content for debugging
                var moviesResponse = JsonConvert.DeserializeObject<PopularMovieApiResponse>(response.Content); // Deserialize the JSON response
                return moviesResponse?.Results; // Return the list of popular movies
            }
            else
            {
                Console.WriteLine("Error: " + response.StatusCode); // Log the error status code if the request fails
                return new List<PopularMovie>(); // Return an empty list in case of error
            }
        }
    }

    public class PopularMovieApiResponse
    {
        [JsonProperty("results")]
        public List<PopularMovie> Results { get; set; } 
    }

    public class PopularMovie
    {
        public int Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("original_title")]
        public string OriginalTitle { get; set; }
        [JsonProperty("overview")]
        public string Overview { get; set; }
        [JsonProperty("release_date")]
        public string ReleaseDate { get; set; }
        [JsonProperty("vote_average")]
        public double VoteAverage { get; set; }
        [JsonProperty("vote_count")]
        public int VoteCount { get; set; }
        [JsonProperty("poster_path")]
        public string PosterPath { get; set; }
    }
}

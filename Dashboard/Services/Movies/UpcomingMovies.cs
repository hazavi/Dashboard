using RestSharp;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dashboard.Services.Movies
{
    // Service class for interacting with the Movie Database API to retrieve upcoming movies
    public class UpcomingMovies
    {
        private readonly RestClient _client;
        private const string BearerToken = "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiIwMTExYTliOTgzNmIyM2JkN2NlNWVjMjNhNDMyYWM0NCIsIm5iZiI6MTcyNDczNzYwNS4wODM2MTIsInN1YiI6IjY2Y2MyYWQ2ZDZjOWM0MzliMzFkNzI1MCIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.zblG2STINV80mvXBwijgedtIRy_Zst8UjN14gi1dEFg"; // API key for authentication

        // Constructor initializes the RestClient with the base API URL
        public UpcomingMovies()
        {
            var options = new RestClientOptions("https://api.themoviedb.org/3/");
            _client = new RestClient(options);
        }

        // Fetches a list of upcoming movies from the API
        public async Task<List<UpcomingMovie>> GetUpcomingMoviesAsync()
        {
            var request = new RestRequest("movie/upcoming", Method.Get); // Create request for upcoming movies
            request.AddParameter("language", "en-US"); // Add language parameter
            request.AddParameter("page", "1"); // Add page parameter
            request.AddHeader("accept", "application/json"); // Set accept header
            request.AddHeader("Authorization", $"Bearer {BearerToken}"); // Set authorization header with the Bearer token

            var response = await _client.ExecuteAsync(request); // Send the request asynchronously(network request: a way that allows your program to continue executing other tasks while waiting for the request to complete )

            if (response.IsSuccessful)
            {
                Console.WriteLine("Response Content: " + response.Content); // Log the response content for debugging
                var moviesResponse = JsonConvert.DeserializeObject<UpcomingMovieApiResponse>(response.Content); // Deserialize JSON response
                return moviesResponse?.Results; // Return the list of upcoming movies
            }
            else
            {
                Console.WriteLine("Error: " + response.StatusCode); // Log error status code
                return new List<UpcomingMovie>(); // Return an empty list in case of an error
            }
        }
    }

    // Response model for the API response 
    public class UpcomingMovieApiResponse
    {
        [JsonProperty("results")]
        public List<UpcomingMovie> Results { get; set; }
    }

    public class UpcomingMovie
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

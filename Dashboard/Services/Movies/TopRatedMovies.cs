using RestSharp;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dashboard.Services.Movies
{
    // Service class to interact with the Movie Database API for fetching top-rated movies
    public class TopRatedMovies
    {
        private readonly RestClient _client; // RestClient instance for making API requests
        private const string BearerToken = "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiIwMTExYTliOTgzNmIyM2JkN2NlNWVjMjNhNDMyYWM0NCIsIm5iZiI6MTcyNDczNzYwNS4wODM2MTIsInN1YiI6IjY2Y2MyYWQ2ZDZjOWM0MzliMzFkNzI1MCIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.zblG2STINV80mvXBwijgedtIRy_Zst8UjN14gi1dEFg"; // API key for authorization

        // Constructor to initialize(setting up) RestClient with the base URL
        public TopRatedMovies()
        {
            var options = new RestClientOptions("https://api.themoviedb.org/3/");
            _client = new RestClient(options);
        }

        // fetches top-rated movies from the API
        public async Task<List<TopRatedMovie>> GetTopRatedMoviesAsync()
        {
            // Create a new RestRequest for the top-rated movies endpoint
            var request = new RestRequest("movie/top_rated", Method.Get);
            request.AddParameter("language", "en-US"); // Specify the language for the results
            request.AddParameter("page", "1"); // Request the first page of results
            request.AddHeader("accept", "application/json"); // Accept JSON response
            request.AddHeader("Authorization", $"Bearer {BearerToken}"); // Add authorization header

            // Send the request and get the response
            var response = await _client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                // Log the response content for debugging
                Console.WriteLine("Response Content: " + response.Content);

                // Deserialize the JSON response into a TopRatedMovieApiResponse object
                var moviesResponse = JsonConvert.DeserializeObject<TopRatedMovieApiResponse>(response.Content);
                return moviesResponse?.Results; // Return the list of top-rated movies
            }
            else
            {
                // Log the error status code
                Console.WriteLine("Error: " + response.StatusCode);
                return new List<TopRatedMovie>(); // Return an empty list on error
            }
        }
    }

    // Response model classes for the API response 
    public class TopRatedMovieApiResponse
    {
        [JsonProperty("results")]
        public List<TopRatedMovie> Results { get; set; } // List of top-rated movies
    }

    public class TopRatedMovie
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

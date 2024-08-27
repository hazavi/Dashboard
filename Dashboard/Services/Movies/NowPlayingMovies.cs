using RestSharp;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dashboard.Services.Movies
{
    public class NowPlayingMovies
    {
        private readonly RestClient _client; // RestClient instance to handle HTTP requests
        private const string BearerToken = "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiIwMTExYTliOTgzNmIyM2JkN2NlNWVjMjNhNDMyYWM0NCIsIm5iZiI6MTcyNDczNzYwNS4wODM2MTIsInN1YiI6IjY2Y2MyYWQ2ZDZjOWM0MzliMzFkNzI1MCIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.zblG2STINV80mvXBwijgedtIRy_Zst8UjN14gi1dEFg"; // API key for authorization

        public NowPlayingMovies()
        {
            var options = new RestClientOptions("https://api.themoviedb.org/3/");
            _client = new RestClient(options); // Initialize RestClient with the base API URL
        }

        // Fetches the list of currently playing movies
        public async Task<List<NowPlayingMovie>> GetNowPlayingMoviesAsync()
        {
            var request = new RestRequest("movie/now_playing", Method.Get); // Create a GET request for the "now_playing" endpoint
            request.AddParameter("language", "en-US"); // Set the language parameter
            request.AddParameter("page", "1"); // Set the page parameter
            request.AddHeader("accept", "application/json"); // Set the Accept header to specify JSON response
            request.AddHeader("Authorization", $"Bearer {BearerToken}"); // Add the Authorization header with the Bearer token

            var response = await _client.ExecuteAsync(request); // Send the request and wait for the response

            if (response.IsSuccessful)
            {
                Console.WriteLine("Response Content: " + response.Content); // Log the response content for debugging
                var moviesResponse = JsonConvert.DeserializeObject<NowPlayingMovieApiResponse>(response.Content); // Deserialize the response content
                return moviesResponse?.Results; // Return the list of movies if successful
            }
            else
            {
                Console.WriteLine("Error: " + response.StatusCode); // Log the error status code if the request fails
                return new List<NowPlayingMovie>(); // Return an empty list in case of an error
            }
        }
    }

    public class NowPlayingMovieApiResponse
    {
        [JsonProperty("results")]
        public List<NowPlayingMovie> Results { get; set; } 
    }

    public class NowPlayingMovie
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

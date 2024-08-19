namespace WeatherAPI.Models
{
    public class WeatherData
    {
        public int Id { get; set; }
        public string City { get; set; }
        public float Temperature { get; set; }
        public DateTime Timestamp { get; set; }
        
        public int RoundedTemperature => (int)Math.Round(Temperature);

    }
}

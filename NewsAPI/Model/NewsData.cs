namespace NewsAPI.Model
{
    public class NewsData
    {
    }

    public class NewsResponse
    {
        public TopNews[] TopNews { get; set; }
    }

    public class TopNews
    {
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
        public string Language { get; set; }
        public string SourceCountry { get; set; }
        public double Sentiment { get; set; }
    }

}

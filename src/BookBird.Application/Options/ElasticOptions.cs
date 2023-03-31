namespace BookBird.Application.Options
{
    public class ElasticOptions
    {
        public string Url { get; set; }
        public string Index { get; set; }
        public bool EnableDebugMode { get; set; }
        public int RequestTimeoutSec { get; set; }
    }
}
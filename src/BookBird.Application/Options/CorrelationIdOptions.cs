namespace BookBird.Application.Options
{
    public class CorrelationIdOptions
    {
        public string Header { get; set; } = Constants.CorrelationIdHeader;
        public bool IncludeInResponse { get; set; } = true;
    }
}
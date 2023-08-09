namespace StockQuoteJob.Domain.Entities
{
    public class StockQuoteRequest
    {
        public string? Ticker { get; set; }
        public string? Range { get; set; }
        public string? Interval { get; set; }
        public bool Fundamental { get; set; }
        public bool Dividends { get; set; }
    }
}

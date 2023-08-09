namespace StockQuoteJob.Domain.Entities
{
    public class StockQuoteResponse
    {
        public List<Result>? Results { get; set; }
        public DateTime RequestedAt { get; set; }
    }

    public class Result
    {
        public string? Symbol { get; set; }
        public List<HistoricalDataPrice>? HistoricalDataPrice { get; set; }
    }

    public class HistoricalDataPrice
    {
        public int? Date { get; set; }
        public double? Close { get; set; }
    }
}

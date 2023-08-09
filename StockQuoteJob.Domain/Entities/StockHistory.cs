namespace StockQuoteJob.Domain.Entities
{
    public class StockHistory
    {
        public string? Stock { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }
    }
}

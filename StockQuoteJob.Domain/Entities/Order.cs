namespace StockQuoteJob.Domain.Entities
{
    public class Order
    {
        public string? Stock { get; set; }
        public double SellPrice { get; set; }
        public double BuyPrice { get; set; }
    }
}

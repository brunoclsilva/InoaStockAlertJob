namespace StockQuoteJob.Domain.Entities
{
    public class EmailConfiguration
    {
        public string? EmailReceiver { get; set; }
        public string? EmailSender { get; set; }
        public string? Host { get; set; }
        public int Port { get; set; }
        public string? ApiKey { get; set; }
        public string? SecretKey { get; set; }
    }
}

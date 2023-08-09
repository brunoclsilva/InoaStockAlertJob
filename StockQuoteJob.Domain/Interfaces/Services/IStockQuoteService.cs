namespace StockQuoteJob.Domain.Interfaces.Services
{
    public interface IStockQuoteService
    {
        Task<double?> GetCurrentPrice(string stock);
    }
}

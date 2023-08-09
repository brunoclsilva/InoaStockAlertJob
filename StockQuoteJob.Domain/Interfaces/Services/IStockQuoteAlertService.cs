using StockQuoteJob.Domain.Entities;

namespace StockQuoteJob.Domain.Interfaces.Services
{
    public interface IStockQuoteAlertService
    {
        Task ProcessQuote(Order order);
    }
}

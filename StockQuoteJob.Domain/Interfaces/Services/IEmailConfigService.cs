using StockQuoteJob.Domain.Entities;

namespace StockQuoteJob.Domain.Interfaces.Services
{
    public interface IEmailConfigService
    {
        Task<EmailConfiguration?> ReadJsonFile(string path);
    }
}

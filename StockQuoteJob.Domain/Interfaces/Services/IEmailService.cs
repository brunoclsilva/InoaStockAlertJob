using StockQuoteJob.Domain.Entities;
using StockQuoteJob.Domain.Enums;

namespace StockQuoteJob.Domain.Interfaces.Services
{
    public interface IEmailService
    {
        Task Send(EmailConfiguration emailConfiguration, string stock, double? currentValue, OperationType operationType);
    }
}

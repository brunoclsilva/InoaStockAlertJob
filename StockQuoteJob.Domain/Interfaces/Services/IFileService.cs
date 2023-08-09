namespace StockQuoteJob.Domain.Interfaces.Services
{
    public interface IFileService
    {
        Task Save<T>(T entity);
    }
}

namespace StockQuoteJob.Domain.Interfaces.Repositories.Base
{
    public interface IRepositoryBase
    {
        T Get<T>();
        T GetById<T>(int id);
        Task Save<T>(T entity);
        Task Delete<T>(int id);
        Task Update<T>(T entity);
    }
}

using StockQuoteJob.Domain.Interfaces.Repositories;
using System.Data.SqlClient;

namespace StockQuoteJob.Infra.Data.Repositories
{
    public class SqlServerRepository : ISqlServerRepository
    {
        private readonly SqlConnection _connection;

        public SqlServerRepository(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public Task Delete<T>(int id)
        {
            throw new NotImplementedException();
        }

        public T Get<T>()
        {
            throw new NotImplementedException();
        }

        public T GetById<T>(int id)
        {
            throw new NotImplementedException();
        }

        public Task Save<T>(T entity)
        {
            throw new NotImplementedException();
        }

        public Task Update<T>(T entity)
        {
            throw new NotImplementedException();
        }
    }
}

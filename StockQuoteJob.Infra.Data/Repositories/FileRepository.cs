using StockQuoteJob.Domain.Interfaces.Repositories;
using System.Reflection;

namespace StockQuoteJob.Infra.Data.Repositories
{
    public class FileRepository : IFileRepository
    {
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
            string filePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\Historic\stock_historic.txt"));
            using (StreamWriter writer = File.AppendText(filePath))
            {
                Type entityType = entity.GetType();
                PropertyInfo[] properties = entityType.GetProperties();

                foreach (PropertyInfo property in properties)
                {
                    string propertyName = property.Name;
                    object propertyValue = property.GetValue(entity);

                    string formattedLine = $"{propertyName}: {propertyValue} ";
                    writer.Write(formattedLine);
                }
                writer.WriteLine(String.Empty);
            }

            return Task.FromResult(0);
        }

        public Task Update<T>(T entity)
        {
            throw new NotImplementedException();
        }
    }
}

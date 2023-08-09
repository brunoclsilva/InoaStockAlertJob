using StockQuoteJob.Domain.Interfaces.Repositories;
using StockQuoteJob.Domain.Interfaces.Services;

namespace StockQuoteJob.Service.Services
{
    public class FileService : IFileService
    {
        private readonly IFileRepository _fileRepository;

        public FileService(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public Task Save<T>(T entity)
        {
            return _fileRepository.Save(entity);
        }
    }
}

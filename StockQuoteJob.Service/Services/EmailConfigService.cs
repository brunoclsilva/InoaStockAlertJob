using StockQuoteJob.Domain.Entities;
using StockQuoteJob.Domain.Interfaces.Services;
using System.Text;
using System.Text.Json;

namespace StockQuoteJob.Service.Services
{
    public class EmailConfigService : IEmailConfigService
    {
        public async Task<EmailConfiguration?> ReadJsonFile(string path)
        {
            string text = await File.ReadAllTextAsync(path);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var emailConfiguration = await JsonSerializer.DeserializeAsync<EmailConfiguration>(new MemoryStream(Encoding.UTF8.GetBytes(text)), options);
            return emailConfiguration;
        }
    }
}
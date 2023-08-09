using Microsoft.Extensions.Logging;
using StockQuoteJob.Domain.Entities;
using StockQuoteJob.Domain.Enums;
using StockQuoteJob.Domain.Interfaces.Services;

namespace StockQuoteJob.Service.Services
{
    public class StockQuoteAlertService : IStockQuoteAlertService
    {
        private string emailConfigFilePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\Config\email_config.json"));

        private readonly IEmailConfigService _emailConfigService;
        private readonly IStockQuoteService _stockQuoteService;
        private readonly IEmailService _emailService;
        private readonly IFileService _fileService;
        private readonly ILogger<StockQuoteAlertService> _logger;

        public StockQuoteAlertService(IEmailConfigService emailConfigService, 
                                      IStockQuoteService stockQuoteService, 
                                      IEmailService emailService,
                                      IFileService fileService,
                                      ILogger<StockQuoteAlertService> logger)
        {
            _emailConfigService = emailConfigService;
            _stockQuoteService = stockQuoteService;
            _emailService = emailService;
            _fileService = fileService;
            _logger = logger;
        }
        
        public async Task ProcessQuote(Order order)
        {
            if (!string.IsNullOrEmpty(order.Stock))
            {
                _logger.LogInformation($"Getting current quote value...");
                var currentValue = await _stockQuoteService.GetCurrentPrice(order.Stock);

                var stockHistory = new StockHistory()
                {
                    Stock = order.Stock,
                    Date = DateTime.Now,
                    Price = currentValue.Value
                };

                _logger.LogInformation($"Saving stock price on history file...");
                await _fileService.Save(stockHistory);

                if (currentValue <= order.BuyPrice || currentValue >= order.SellPrice)
                {
                    _logger.LogInformation($"Getting configurations...");
                    var emailConfig = await _emailConfigService.ReadJsonFile(emailConfigFilePath);

                    if (emailConfig != null)
                    {
                        _logger.LogInformation($"Sending email...");
                        switch (currentValue)
                        {
                            case var _ when currentValue <= order.BuyPrice:
                                await _emailService.Send(emailConfig, order.Stock, currentValue, OperationType.Buy);
                                break;
                            case var _ when currentValue >= order.SellPrice:
                                await _emailService.Send(emailConfig, order.Stock, currentValue, OperationType.Sell);
                                break;
                        }
                    }
                }
            }

            return;
        }
    }
}

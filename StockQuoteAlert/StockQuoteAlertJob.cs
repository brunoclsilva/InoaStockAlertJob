using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StockQuoteJob.Domain.Entities;
using StockQuoteJob.Domain.Interfaces.Services;
using System.Globalization;

namespace StockQuoteAlert.Application
{
    public class StockQuoteAlertJob : BackgroundService
    {
        private readonly string[] _args;
        private readonly ILogger<StockQuoteAlertJob> _logger;
        private readonly IStockQuoteAlertService _stockQuoteAlertService;
        
        public StockQuoteAlertJob(ILogger<StockQuoteAlertJob> logger, string[] args, IStockQuoteAlertService stockQuoteAlertService)
        {
            _logger = logger;
            _args = args;
            _stockQuoteAlertService = stockQuoteAlertService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                if (_args.Length >= 3)
                {
                    var order = new Order()
                    {
                        Stock = _args[0],
                        SellPrice = Convert.ToDouble(_args[1], CultureInfo.GetCultureInfo("en-US")),
                        BuyPrice = Convert.ToDouble(_args[2], CultureInfo.GetCultureInfo("en-US")),
                    };

                    _logger.LogInformation("StockQuoteJob service started.");
                    _logger.LogInformation($"Stock: {order.Stock}");
                    _logger.LogInformation($"Sell Price: {order.SellPrice}");
                    _logger.LogInformation($"Buy Price: {order.BuyPrice}");

                    while (!stoppingToken.IsCancellationRequested)
                    {
                        _logger.LogInformation("StockQuoteJob service is running! Press ctrl+c to stop...");

                        await _stockQuoteAlertService.ProcessQuote(order);

                        await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
                    }

                    _logger.LogInformation("StockQuoteJob service stopped.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"StockQuoteJob service failed with error: {ex.Message}");
            }
        }
    }
}

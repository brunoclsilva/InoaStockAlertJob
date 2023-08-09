using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StockQuoteJob.Domain.Entities;
using StockQuoteJob.Domain.Interfaces.Services;

namespace StockQuoteJob.Service.Services
{
    public class StockQuoteService : IStockQuoteService
    {
        string apiUrlBase = "https://brapi.dev/api/quote/";
        private readonly ILogger<StockQuoteService> _logger;

        public StockQuoteService(ILogger<StockQuoteService> logger)
        {
            _logger = logger;
        }

        public async Task<double?> GetCurrentPrice(string stock)
        {
            var request = new StockQuoteRequest { 
                Ticker = stock,
                Range = "1d",
                Interval = "1m",
                Fundamental = false,
                Dividends = false
            };

            double? price = null;
            try
            {
                _logger.LogInformation($"Calling quote API...");
                var apiUrl = apiUrlBase + $"{request.Ticker}?range={request.Range}&interval={request.Interval}&fundamental={request.Fundamental}&dividends={request.Dividends}";
                using HttpClient httpClient = new HttpClient();
                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var stockQuoteResponse = JsonConvert.DeserializeObject<StockQuoteResponse>(responseBody);

                    _logger.LogInformation($"API Response: {response.StatusCode}");

                    price = GetCurrentPrice(stockQuoteResponse);
                }
                else
                {
                    _logger.LogError($"API call failed with status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred: {ex.Message}");
            }

            return price; 
        }

        private double? GetCurrentPrice(StockQuoteResponse stockQuoteResponse)
        {
            double? currentPrice = null;
            var result = stockQuoteResponse?.Results?.FirstOrDefault();
            if (result != null)
            {
                var lastestHistorical = result?.HistoricalDataPrice?.OrderByDescending(o => o.Date)?.FirstOrDefault();
                currentPrice = lastestHistorical?.Close;

                _logger.LogInformation($"LatestQuote: {DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(lastestHistorical?.Date)).DateTime} - CurrentPrice: {currentPrice}");
            }

            return currentPrice;
        }
    }
}

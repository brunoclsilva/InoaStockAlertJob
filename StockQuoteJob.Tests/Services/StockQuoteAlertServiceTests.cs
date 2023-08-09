using Microsoft.Extensions.Logging;
using Moq;
using StockQuoteJob.Domain.Entities;
using StockQuoteJob.Domain.Enums;
using StockQuoteJob.Domain.Interfaces.Services;
using StockQuoteJob.Service.Services;

namespace StockQuoteJob.Tests.Services
{
    public class StockQuoteAlertServiceTests
    {
        private readonly Mock<IEmailConfigService> _emailConfigMock;
        private readonly Mock<IStockQuoteService> _stockMock;
        private readonly Mock<IEmailService> _emailMock;
        private readonly Mock<ILogger<StockQuoteAlertService>> _loggerMock;

        private EmailConfiguration _emailConfig;
        private string _stock = "PETR4";

        public StockQuoteAlertServiceTests()
        {
            _emailConfigMock = new Mock<IEmailConfigService>();
            _stockMock = new Mock<IStockQuoteService>();
            _emailMock = new Mock<IEmailService>();
            _loggerMock = new Mock<ILogger<StockQuoteAlertService>>();

            _emailConfig = new EmailConfiguration();
        }

        [Fact]
        public async Task ProcessQuote_Sends_Email_When_CurrentValue_Less_Than_BuyPrice()
        {
            // Arrange
            _emailConfigMock.Setup(x => x.ReadJsonFile(It.IsAny<string>())).ReturnsAsync(_emailConfig);
            var order = new Order { Stock = _stock, BuyPrice = 100.0 };
            var currentValue = 95.0;
            _stockMock.Setup(x => x.GetCurrentPrice(_stock)).ReturnsAsync(currentValue);
            var service = new StockQuoteAlertService(_emailConfigMock.Object, _stockMock.Object, _emailMock.Object, _loggerMock.Object);

            await service.ProcessQuote(order);

            _emailMock.Verify(x => x.Send(_emailConfig, _stock, currentValue, OperationType.Buy), Times.Once);
        }

        [Fact]
        public async Task ProcessQuote_Sends_Email_When_CurrentValue_Greater_Than_SellPrice()
        {
            _emailConfigMock.Setup(x => x.ReadJsonFile(It.IsAny<string>())).ReturnsAsync(_emailConfig);
            var order = new Order { Stock = _stock, SellPrice = 150.0 };
            var currentValue = 155.0;
            _stockMock.Setup(x => x.GetCurrentPrice(_stock)).ReturnsAsync(currentValue);
            var service = new StockQuoteAlertService(_emailConfigMock.Object, _stockMock.Object, _emailMock.Object, _loggerMock.Object);

            await service.ProcessQuote(order);

            _emailMock.Verify(x => x.Send(_emailConfig, _stock, currentValue, OperationType.Sell), Times.Once);
        }

        [Fact]
        public async Task ProcessQuote_Does_Not_Send_Email_When_CurrentValue_Between_BuyPrice_And_SellPrice()
        {
            _emailConfigMock.Setup(x => x.ReadJsonFile(It.IsAny<string>())).ReturnsAsync(_emailConfig);
            var order = new Order { Stock = _stock, BuyPrice = 100.0, SellPrice = 150.0 };
            var currentValue = 120.0; // Replace with a value between the buy and sell prices.
            _stockMock.Setup(x => x.GetCurrentPrice(_stock)).ReturnsAsync(currentValue);
            var service = new StockQuoteAlertService(_emailConfigMock.Object, _stockMock.Object, _emailMock.Object, _loggerMock.Object);

            await service.ProcessQuote(order);

            _emailMock.Verify(x => x.Send(It.IsAny<EmailConfiguration>(), It.IsAny<string>(), It.IsAny<double>(), It.IsAny<OperationType>()), Times.Never);
        }
    }
}

using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using StockQuoteJob.Domain.Entities;
using StockQuoteJob.Service.Services;
using System.Net;

namespace StockQuoteJob.Tests.Services
{
    public class StockQuoteServiceTests
    {
        [Fact]
        public async Task GetCurrentPrice_Returns_CurrentPrice_Successfully()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<StockQuoteService>>();
            var stock = "PETR4";

            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
            var handlerMock = new Mock<HttpMessageHandler>();

            handlerMock.Protected().Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()).ReturnsAsync(responseMessage).Verifiable();

            var service = new StockQuoteService(loggerMock.Object);

            // Act
            var currentPrice = await service.GetCurrentPrice(stock);

            // Assert
            Assert.NotNull(currentPrice);
        }
    }
}

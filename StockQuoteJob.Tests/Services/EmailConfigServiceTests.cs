using StockQuoteJob.Service.Services;

namespace StockQuoteJob.Tests.Services
{
    public class EmailConfigServiceTests
    {
        [Fact]
        public async Task ReadJsonFile_Returns_EmailConfiguration_Object()
        {
            // Arrange
            var jsonContent = "{\"SmtpServer\":\"smtp.example.com\",\"Port\":587,\"UserName\":\"user@example.com\",\"Password\":\"yourpassword\",\"FromAddress\":\"noreply@example.com\"}";

            // Create a temporary file with the JSON content
            var tempFilePath = Path.GetTempFileName();
            await File.WriteAllTextAsync(tempFilePath, jsonContent);

            var service = new EmailConfigService();

            try
            {
                // Act
                var emailConfig = await service.ReadJsonFile(tempFilePath);

                // Assert
                Assert.NotNull(emailConfig);
            }
            finally
            {
                // Cleanup: Delete the temporary file
                File.Delete(tempFilePath);
            }
        }
    }
}

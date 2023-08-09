using StockQuoteJob.Domain.Entities;
using StockQuoteJob.Domain.Interfaces.Services;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Logging;
using StockQuoteJob.Domain.Enums;
using System.Net.Http;

namespace StockQuoteJob.Service.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        private readonly SmtpClient _smtpClient;
        public EmailService(ILogger<EmailService> logger, SmtpClient smtpClient)
        {
            _logger = logger;
            _smtpClient = smtpClient;
        }
        public async Task Send(EmailConfiguration emailConfiguration, string stock, double? currentValue, OperationType operationType)
        {
            try
            {
                var action = EnumExtension.GetEnumDescription(operationType);
                string subject = $"Lembrete de {action} da ação {stock}.";
                string body = $"Olá, a ação {stock} chegou ao preço de R${currentValue.Value.ToString("0.##")} e esse é um lembrete para {action}!";

                if (ValidateParameters(emailConfiguration))
                {
                    using (MailMessage mailMessage = new(emailConfiguration.EmailSender, emailConfiguration.EmailReceiver, subject, body))
                    {
                        _smtpClient.Host = emailConfiguration.Host;
                        _smtpClient.Port = emailConfiguration.Port;
                        _smtpClient.EnableSsl = true;
                        _smtpClient.Credentials = new NetworkCredential(emailConfiguration.ApiKey, emailConfiguration.SecretKey);

                        await _smtpClient.SendMailAsync(mailMessage);

                        _logger.LogInformation("Email sent successfully!");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to send email: {ex.Message}");
            }
        }

        private bool ValidateParameters(EmailConfiguration emailConfiguration)
        {
            return !string.IsNullOrEmpty(emailConfiguration?.EmailSender) &&
                   !string.IsNullOrEmpty(emailConfiguration?.EmailReceiver) &&
                   !string.IsNullOrEmpty(emailConfiguration?.Host) &&
                   !string.IsNullOrEmpty(emailConfiguration?.ApiKey) &&
                   !string.IsNullOrEmpty(emailConfiguration?.SecretKey) &&
                   emailConfiguration.Port != 0;
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StockQuoteAlert.Application;
using StockQuoteJob.Domain.Interfaces.Repositories;
using StockQuoteJob.Domain.Interfaces.Services;
using StockQuoteJob.Infra.Data.Repositories;
using StockQuoteJob.Service.Services;
using System.Net.Mail;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton(args);

                RegisterServices(ref services);
                RegisterRepositories(ref services);
            });

    public static void RegisterServices(ref IServiceCollection services)
    {
        services.AddHostedService<StockQuoteAlertJob>();
        services.AddScoped<SmtpClient>();
        services.AddScoped<IStockQuoteAlertService, StockQuoteAlertService>();
        services.AddScoped<IEmailConfigService, EmailConfigService>();
        services.AddScoped<IStockQuoteService, StockQuoteService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IFileService, FileService>();
    }

    public static void RegisterRepositories(ref IServiceCollection services)
    {
        services.AddScoped<IFileRepository, FileRepository>();
        services.AddScoped<ISqlServerRepository, SqlServerRepository>();
    }
}
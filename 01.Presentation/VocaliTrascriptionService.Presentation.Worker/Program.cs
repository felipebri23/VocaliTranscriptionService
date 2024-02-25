
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Logging;
using Serilog.Sinks.SystemConsole.Themes;
using VocaliTranscriptionService.Application.Interfaces.Services;
using VocaliTranscriptionService.Application.Services.Services;
using VocaliTranscriptionService.Domain.Entities;
using VocaliTranscriptionService.Domain.Repositories;
using VocaliTranscriptionService.Infrastructure.Data.Repositories;
using VocaliTranscriptionService.Presentation.Worker;
using VocaliTranscriptionService.Presentation.Worker.Validations;
using ILogger = Serilog.ILogger;

internal class Program
{
    private static void Main(string[] args)
    {
        try
        {
            Log.Logger = CreateLogger();
            CreateHostBuilder(args).Build().Run();
        }
        catch (Exception ex)
        {
            Log.ForContext<Program>().Fatal(ex, $"Host terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
           .ConfigureAppConfiguration((buildercontext, config) =>
           {
               config.AddJsonFile(path: Path.Combine(AppContext.BaseDirectory, "Config", "appsettings.json"), optional: true, reloadOnChange: true);
           })
           .ConfigureServices((hostContext, services) =>
           {
               services.AddSingleton<ILoggerFactory>(services => new SerilogLoggerFactory(CreateLogger(), false));
               services.AddScoped<IFileService, FileService>();
               services.AddScoped<IFileModelRepository, FileModelRepository>();
               services.AddScoped<ITranscriptedFileRepository, TranscriptedFileRepository>();
               services.AddScoped<IValidator<FileModel>, FileModelValidator>();
               services.AddSingleton(hostContext.Configuration);
               services.AddHostedService<Worker>();
           });

    private static ILogger CreateLogger()
    {
        var loggerConfiguration = new LoggerConfiguration()
            .WriteTo.Console(theme: AnsiConsoleTheme.Code)  
            .ReadFrom.Configuration(GetAppConfiguration());

        return loggerConfiguration.CreateLogger();
    }

    private static IConfiguration GetAppConfiguration()
    {
        // Actually, before ASP.NET bootstrap, we must rely on environment variable to get environment name
        // https://docs.microsoft.com/fr-fr/aspnet/core/fundamentals/environments?view=aspnetcore-2.2
        // Pay attention to casing for Linux environment. By default it's pascal case.

        return new ConfigurationBuilder()
            .AddJsonFile(path: Path.Combine(AppContext.BaseDirectory, "appsettings.json"), optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();
    }
}
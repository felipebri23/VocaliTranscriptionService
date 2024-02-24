using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using VocaliTranscriptionService.Application.Interfaces.Services;

namespace VocaliTranscriptionService.Presentation.Worker
{
    public class Worker : BackgroundService
    {
        private readonly IFileService _fileService;

        private readonly ILogger<Worker> _logger;

        private readonly IConfiguration _configuration;

        public Worker(IFileService fileService, ILogger<Worker> logger, IConfiguration configuration)
        {
            _fileService = fileService;
            _logger = logger;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"Worker running at {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}!");

            const int maxProcessingInParallel = 3;

            var configurationPath = _configuration["filesPath"];

            if (string.IsNullOrEmpty(configurationPath))
            {
                _logger.LogError("Path not configured");

            } 
            else
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        var pendingFiles = await _fileService.GetFiles(configurationPath);
                        var processing = 0;

                        Parallel.ForEach(pendingFiles, (pendingFile) =>
                        {
                            while (processing == maxProcessingInParallel) 
                            {
                                Task.Delay(1000);
                            }

                            processing++;
                            // Llamamos al servicio de trasncripción
                            processing--;
                        });
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }           

            _logger.LogInformation($"Worker end at {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}!\"");
        }
    }
}

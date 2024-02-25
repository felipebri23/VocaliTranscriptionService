using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using VocaliTranscriptionService.Application.Interfaces.Services;
using VocaliTranscriptionService.Domain.Entities;

namespace VocaliTranscriptionService.Presentation.Worker
{
    public class Worker : BackgroundService
    {
        private readonly IFileService _fileService;

        private readonly ILogger<Worker> _logger;

        private readonly IConfiguration _configuration;

        private IValidator<FileModel> _validator;

        public Worker(IFileService fileService, ILogger<Worker> logger, IConfiguration configuration, IValidator<FileModel> validator)
        {
            _fileService = fileService;
            _logger = logger;
            _configuration = configuration;
            _validator = validator;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"Worker running at {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}!");

            const int maxProcessingInParallel = 3;
            const int maxProcessingFileCount = 3;

            var configurationPath = _configuration["filesPath"];
            var transcriptFileServerUrl = _configuration["transcriptFileServerUrl"];

            if (string.IsNullOrEmpty(configurationPath))
            {
                _logger.LogError("Path not configured");
                Environment.Exit(1);
            }

            if (string.IsNullOrEmpty(transcriptFileServerUrl))
            {
                _logger.LogError("TranscriptFileServerUrl not configured");
                Environment.Exit(2);
            }

            var pendingFiles = await _fileService.GetFiles(configurationPath);
            var processing = 0;

            Parallel.ForEach(pendingFiles, (pendingFile) =>
            {
                var proccesingFileCount = 0;
                bool isValid = false;

                var validationResult = _validator.Validate(pendingFile);
                while (processing >= maxProcessingInParallel) 
                {
                    Task.Delay(1000);
                }

                processing++;

                while (proccesingFileCount < maxProcessingFileCount && !isValid)
                {
                    proccesingFileCount++;
                    try
                    {
                        _fileService.TranscriptFile(pendingFile, transcriptFileServerUrl, configurationPath);
                        isValid = true;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Error in file {pendingFile.Filename}, {ex.Message}");
                        throw;
                    }
                }

                processing--;
            });       

            _logger.LogInformation($"Worker end at {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}!\"");
            //Environment.Exit(0);
        }
    }
}

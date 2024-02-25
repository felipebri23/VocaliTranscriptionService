using System.Text;
using VocaliTranscriptionService.Application.Interfaces.Services;
using VocaliTranscriptionService.Domain.Entities;
using VocaliTranscriptionService.Domain.Repositories;

namespace VocaliTranscriptionService.Application.Services.Services
{
    public class FileService : IFileService
    {
        private readonly IFileModelRepository _fileModelRepository;
        private readonly ITranscriptedFileRepository _transcriptedFileRepository;

        public FileService(
            IFileModelRepository fileModelRepository, 
            ITranscriptedFileRepository transcriptedFileRepository)
        {
            _fileModelRepository = fileModelRepository;
            _transcriptedFileRepository = transcriptedFileRepository;
        }

        public async Task<IEnumerable<FileModel>> GetFiles(string path)
        {
            return await  _fileModelRepository.GetFiles(path);
        }

        public async Task TranscriptFile(FileModel file, string transcriptFileServerUrl, string path)
        {
            var transcriptedFile = await _transcriptedFileRepository.TranscriptFile(
                file.FileContent, 
                transcriptFileServerUrl,
                file.UserId);

            var newFileName = file.Filename.Replace("mp3", "txt");
            var newFileContent = Encoding.ASCII.GetBytes(transcriptedFile.File);

            await _fileModelRepository.CreateTranscriptedFile(path, newFileName, newFileContent);
            _fileModelRepository.DeleteTranscriptedFile(path, file.Filename);
        }
    }
}

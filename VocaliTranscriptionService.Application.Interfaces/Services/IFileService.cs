using VocaliTranscriptionService.Domain.Entities;

namespace VocaliTranscriptionService.Application.Interfaces.Services
{
    public interface IFileService
    {
        Task<IEnumerable<FileModel>> GetFiles(string path);

        Task TranscriptFile(FileModel file, string transcriptFileServerUrl, string path);
    }
}

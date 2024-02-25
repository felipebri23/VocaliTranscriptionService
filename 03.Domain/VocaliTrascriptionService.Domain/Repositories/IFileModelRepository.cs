using VocaliTranscriptionService.Domain.Entities;

namespace VocaliTranscriptionService.Domain.Repositories
{
    public interface IFileModelRepository
    {
        Task<IEnumerable<FileModel>> GetFiles(string path);

        Task CreateTranscriptedFile(string path, string filename, byte[] fileContent);

        void DeleteTranscriptedFile(string path, string filename);
    }
}

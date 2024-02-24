using VocaliTranscriptionService.Domain.Entities;

namespace VocaliTranscriptionService.Domain.Repositories
{
    public interface IFileModelRepository
    {
        Task<IEnumerable<FileModel>> GetFiles(string path);
    }
}

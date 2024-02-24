using VocaliTranscriptionService.Domain.Entities;
using VocaliTranscriptionService.Domain.Repositories;

namespace VocaliTranscriptionService.Infrastructure.Data.Repositories
{
    public class FileModelRepository : IFileModelRepository
    {
        public Task<IEnumerable<FileModel>> GetFiles(string path)
        {
            var files = Directory.GetFiles(path)
                .Select(path => new FileModel(
                        Guid.NewGuid(),
                        File.ReadAllBytes(path),
                        Path.GetFileName(path),
                        Path.GetExtension(path)));

            return Task.FromResult(files);
        }
    }
}

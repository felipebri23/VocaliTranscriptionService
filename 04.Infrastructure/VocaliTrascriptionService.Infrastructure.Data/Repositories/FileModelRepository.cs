using VocaliTranscriptionService.Domain.Entities;
using VocaliTranscriptionService.Domain.Repositories;

namespace VocaliTranscriptionService.Infrastructure.Data.Repositories
{
    public class FileModelRepository : IFileModelRepository
    {
        public Task<IEnumerable<FileModel>> GetFiles(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var files = Directory.GetFiles(path, "*.mp3", SearchOption.TopDirectoryOnly)
                .Select(path =>
                {
                    FileInfo fileInfo = new FileInfo(path);

                    return new FileModel(
                        Guid.NewGuid(),
                        File.ReadAllBytes(path),
                        fileInfo.FullName,
                        fileInfo.Length,
                        fileInfo.Extension);
                });
            return Task.FromResult(files);
        }

        public async Task CreateTranscriptedFile(string path, string filename, byte[] fileContent)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var fullname = Path.Combine(path, filename);

            await File.WriteAllBytesAsync(fullname, fileContent);
        }

        public void DeleteTranscriptedFile(string path, string filename)
        {
            File.Delete(Path.Combine(path, filename));
        }
    }
}

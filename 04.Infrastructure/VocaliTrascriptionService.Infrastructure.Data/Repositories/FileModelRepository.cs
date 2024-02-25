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

            int current = 0;
            
            // En este punto, creo que lo mejor sería guardar, en el momento en el que se guarda el archivo mp3,
            // una referencia del fichero y el usuario propietario en bbdd y obtenerlo de algún repositorio.
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string randomUser = new string(Enumerable.Repeat(chars, 10)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            var files = Directory.GetFiles(path, "*.mp3", SearchOption.TopDirectoryOnly)
                .Select(path =>
                {
                    FileInfo fileInfo = new FileInfo(path);

                    current++;

                    return new FileModel(
                        Guid.NewGuid(),
                        File.ReadAllBytes(path),
                        fileInfo.FullName,
                        fileInfo.Length,
                        fileInfo.Extension, 
                        randomUser);
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

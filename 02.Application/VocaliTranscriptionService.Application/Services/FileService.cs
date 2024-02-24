using VocaliTranscriptionService.Application.Interfaces.Services;
using VocaliTranscriptionService.Domain.Entities;
using VocaliTranscriptionService.Domain.Repositories;

namespace VocaliTranscriptionService.Application.Services.Services
{
    public class FileService : IFileService
    {
        private readonly IFileModelRepository _fileModelRepository;

        public FileService(IFileModelRepository fileModelRepository)
        {
            _fileModelRepository = fileModelRepository;
        }

        public async Task<IEnumerable<FileModel>> GetFiles(string path)
        {
            if (!Directory.Exists(path)) 
            {
                throw new Exception($"El directorio: {path} no existe");
            }

            return await  _fileModelRepository.GetFiles(path);
        }
    }
}

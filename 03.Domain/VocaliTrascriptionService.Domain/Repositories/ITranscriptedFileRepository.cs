using VocaliTranscriptionService.Domain.Entities;

namespace VocaliTranscriptionService.Domain.Repositories
{
    public interface ITranscriptedFileRepository
    {
        Task<TranscriptedFileModel> TranscriptFile(byte[] fileContent, string transcriptFileServerUrl, string userId);
    }
}

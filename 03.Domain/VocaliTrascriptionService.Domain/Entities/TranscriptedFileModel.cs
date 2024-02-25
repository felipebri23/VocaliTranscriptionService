namespace VocaliTranscriptionService.Domain.Entities
{
    public class TranscriptedFileModel
    {
        public TranscriptedFileModel(string file)
        {
            File = file;
        }

        public string File { get; set; }
    }
}

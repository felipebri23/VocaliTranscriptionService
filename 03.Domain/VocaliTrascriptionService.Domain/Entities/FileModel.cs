namespace VocaliTranscriptionService.Domain.Entities
{
    public class FileModel
    {
        public FileModel(
            Guid fileId, 
            byte[] fileContent, 
            string fileName, 
            long fileSize, 
            string extension,
            string userId
            )
        {
            FileId = fileId;
            FileContent = fileContent;
            Filename = fileName;
            FileSize = fileSize;
            FileExtension = extension; 
            UserId = userId;
        }

        public Guid FileId { get; set; }

        public byte[] FileContent { get; set; }

        public string Filename { get; set; }

        public long FileSize { get; set; }

        public string FileExtension { get; set; }

        public string UserId { get; set; }
    }
}

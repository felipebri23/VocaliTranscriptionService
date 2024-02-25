using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VocaliTranscriptionService.Domain.Entities
{
    public class FileModel
    {
        public FileModel(Guid fileId, byte[] fileContent, string fileName, long fileSize, string extension)
        {
            FileId = fileId;
            FileContent = fileContent;
            Filename = fileName;
            FileSize = fileSize;
            FileExtension = extension;
        }

        public Guid FileId { get; set; }

        public byte[] FileContent { get; private set; }

        public string Filename { get; private set; }

        public long FileSize { get; private set; }

        public string FileExtension { get; private set; }
    }
}

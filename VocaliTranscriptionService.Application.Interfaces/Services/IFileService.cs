using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocaliTranscriptionService.Domain.Entities;

namespace VocaliTranscriptionService.Application.Interfaces.Services
{
    public interface IFileService
    {
        Task<IEnumerable<FileModel>> GetFiles(string path);
    }
}

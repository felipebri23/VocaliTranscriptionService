using System.Text;
using System.Text.Json;
using VocaliTranscriptionService.Domain.Entities;
using VocaliTranscriptionService.Domain.Repositories;

namespace VocaliTranscriptionService.Infrastructure.Data.Repositories
{
    public class TranscriptedFileRepository : ITranscriptedFileRepository
    {
        public async Task<TranscriptedFileModel> TranscriptFile(byte[] fileContent, string transcriptFileServerUrl, string userId)
        {
            var httpClient = new HttpClient();

            StringContent jsonContent = new(
               JsonSerializer.Serialize(new
               {
                   file = fileContent
               }),
               Encoding.UTF8,
               "application/json");

            httpClient.DefaultRequestHeaders.Add("userId", userId);

            HttpResponseMessage response = await httpClient.PostAsync(
                Path.Combine(transcriptFileServerUrl),
                jsonContent);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var transcriptedFile = JsonSerializer.Deserialize<TranscriptedFileModel>(jsonResponse, options);

            return transcriptedFile == null 
                ? throw new Exception("The file is not valid") 
                : transcriptedFile;
        }
    }
}

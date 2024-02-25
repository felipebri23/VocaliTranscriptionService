using AutoFixture;
using VocaliTranscriptionService.Domain.Entities;

namespace VocaliTranscriptionService.Domain.Test
{
    [TestClass]
    public class TranscriptedFileModelTest
    {
        private readonly Fixture _fixture;

        public TranscriptedFileModelTest()
        {
            _fixture = new Fixture();
        }

        [TestMethod]
        public void TranscriptedFileModel_WhenInstancing_Ok()
        {
            // Arrange
            TranscriptedFileModel transcriptedFileModel = _fixture.Create<TranscriptedFileModel>();

            // Act
            TranscriptedFileModel sut = new TranscriptedFileModel(transcriptedFileModel.File);

            // Assert
            Assert.AreEqual(sut.File, transcriptedFileModel.File);
        }
    }
}
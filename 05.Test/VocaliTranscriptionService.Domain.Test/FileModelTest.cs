using AutoFixture;
using VocaliTranscriptionService.Domain.Entities;

namespace VocaliTranscriptionService.Domain.Test
{
    [TestClass]
    public class FileModelTest
    {
        private readonly Fixture _fixture;

        public FileModelTest()
        {
            _fixture = new Fixture();
        }

        [TestMethod]
        public void FileModel_WhenInstancing_Ok()
        {
            // Arrange
            FileModel expectedFileModel = _fixture.Create<FileModel>();

            // Act
            FileModel sut = new FileModel(
                expectedFileModel.FileId, 
                expectedFileModel.FileContent, 
                expectedFileModel.Filename,
                expectedFileModel.FileSize,                
                expectedFileModel.FileExtension,
                expectedFileModel.UserId);

            // Assert
            Assert.AreEqual(sut.FileId, expectedFileModel.FileId);
            Assert.AreEqual(sut.FileContent, expectedFileModel.FileContent);
            Assert.AreEqual(sut.Filename, expectedFileModel.Filename);
            Assert.AreEqual(sut.FileSize, expectedFileModel.FileSize);
            Assert.AreEqual(sut.FileExtension, expectedFileModel.FileExtension);
            Assert.AreEqual(sut.UserId, expectedFileModel.UserId);
        }
    }
}
using AutoFixture;
using Moq;
using VocaliTranscriptionService.Application.Services.Services;
using VocaliTranscriptionService.Domain.Entities;
using VocaliTranscriptionService.Domain.Repositories;

namespace VocaliTranscriptionService.Application.Test
{
    [TestClass]
    public class FileServiceTest
    {
        private readonly Fixture _fixture;
        private readonly Mock<IFileModelRepository> _fileModelRepositoryMock;
        private readonly Mock<ITranscriptedFileRepository> _transcriptedFileRepositoryMock;

        public FileServiceTest()
        {
            _fixture = new Fixture();
            _fileModelRepositoryMock = new Mock<IFileModelRepository>();
            _transcriptedFileRepositoryMock = new Mock<ITranscriptedFileRepository>();
        }

        [TestMethod]
        public async Task GetFiles_WhenIsCalled_GetAllFilesInRepo()
        {
            // Arrange
            string path = _fixture.Create<string>();
            IEnumerable<FileModel> expectedFiles = _fixture.Create<IEnumerable<FileModel>>();
            _fileModelRepositoryMock.Setup(x => x.GetFiles(path)).ReturnsAsync(expectedFiles);
            FileService sut = new FileService(_fileModelRepositoryMock.Object, _transcriptedFileRepositoryMock.Object);

            // Act
            IEnumerable<FileModel> result = await sut.GetFiles(path);

            // Assert
            foreach (var fileModel in result)
            {
                FileModel expectedFileModel = expectedFiles.First(x => x.FileId == fileModel.FileId);
                Assert.AreEqual(fileModel.FileId, expectedFileModel.FileId);
                Assert.AreEqual(fileModel.FileContent, expectedFileModel.FileContent);
                Assert.AreEqual(fileModel.Filename, expectedFileModel.Filename);
                Assert.AreEqual(fileModel.FileSize, expectedFileModel.FileSize);
                Assert.AreEqual(fileModel.FileExtension, expectedFileModel.FileExtension);
            }
        }

        [TestMethod]
        public async Task TranscriptFile_WhenIsCalled_GetTranscriptedFile()
        {
            // Arrange
            string path = _fixture.Create<string>();
            string transcriptFileServerUrl = _fixture.Create<string>();
            FileModel anyFile = _fixture.Create<FileModel>();
            TranscriptedFileModel expectedTranscriptedFileModel = _fixture.Create<TranscriptedFileModel>();
            _fileModelRepositoryMock.Setup(x => x.CreateTranscriptedFile(path, It.IsAny<string>(), It.IsAny<byte[]>()));
            _fileModelRepositoryMock.Setup(x => x.DeleteTranscriptedFile(path, anyFile.Filename));
            _transcriptedFileRepositoryMock.Setup(x => x.TranscriptFile(anyFile.FileContent, transcriptFileServerUrl, anyFile.UserId)).ReturnsAsync(expectedTranscriptedFileModel);

            FileService sut = new FileService(_fileModelRepositoryMock.Object, _transcriptedFileRepositoryMock.Object);

            // Act
            await sut.TranscriptFile(anyFile, transcriptFileServerUrl, path);

            // Assert
            Assert.IsNotNull(expectedTranscriptedFileModel);
        }
    }
} 
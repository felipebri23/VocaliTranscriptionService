using AutoFixture;
using VocaliTranscriptionService.Domain.Entities;
using VocaliTranscriptionService.Presentation.Worker.Validations;

namespace VocaliTranscriptionService.Presentation.Worker.Test
{
    [TestClass]
    public class FileModelValidatorTest
    {
        private readonly Fixture _fixture;
        const string ValidExtension = "mp3";
        const string InvalidExtension = "txt";
        const float ValidFileSize = 1000;
        const float InvalidFileSize = 5000;

        public FileModelValidatorTest()
        {
            _fixture = new Fixture();
        }

        [TestMethod]
        public async Task FileModelValidator_WhenValidate_Ok()
        {
            // Arrange
            FileModel anyTranscriptedFileModel = _fixture.Build<FileModel>()
                .With(x => x.FileExtension, ValidExtension)
                .With(x => x.FileSize, ValidFileSize)
                .Create();

            FileModelValidator sut = new FileModelValidator();

            // Act
            var result = await sut.ValidateAsync(anyTranscriptedFileModel);

            // Assert
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public async Task FileModelValidator_WhenValidateWithFileContentNull_Error()
        {
            // Arrange
            FileModel anyTranscriptedFileModel = _fixture.Build<FileModel>()
                .With(x => x.FileExtension, ValidExtension)
                .With(x => x.FileSize, ValidFileSize)
                .Create();

            anyTranscriptedFileModel.FileContent = default;

            FileModelValidator sut = new FileModelValidator();

            // Act
            var result = await sut.ValidateAsync(anyTranscriptedFileModel);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors[0].PropertyName, nameof(anyTranscriptedFileModel.FileContent));
        }

        [TestMethod]
        public async Task FileModelValidator_WhenValidateWithFileNameNull_Error()
        {
            // Arrange
            FileModel anyTranscriptedFileModel = _fixture.Build<FileModel>()
                .With(x => x.FileExtension, ValidExtension)
                .With(x => x.FileSize, ValidFileSize)
                .Create();

            anyTranscriptedFileModel.Filename = default;

            FileModelValidator sut = new FileModelValidator();

            // Act
            var result = await sut.ValidateAsync(anyTranscriptedFileModel);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors[0].PropertyName, nameof(anyTranscriptedFileModel.Filename));
        }

        [TestMethod]
        public async Task FileModelValidator_WhenValidateWithUserIdNull_Error()
        {
            // Arrange
            FileModel anyTranscriptedFileModel = _fixture.Build<FileModel>()
                .With(x => x.FileExtension, ValidExtension)
                .With(x => x.FileSize, ValidFileSize)
                .Create();

            anyTranscriptedFileModel.UserId = default;

            FileModelValidator sut = new FileModelValidator();

            // Act
            var result = await sut.ValidateAsync(anyTranscriptedFileModel);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors[0].PropertyName, nameof(anyTranscriptedFileModel.UserId));
        }

        [TestMethod]
        public async Task FileModelValidator_WhenValidateWithFileExtensionNull_Error()
        {
            // Arrange
            FileModel anyTranscriptedFileModel = _fixture.Build<FileModel>()
                .With(x => x.FileExtension, ValidExtension)
                .With(x => x.FileSize, ValidFileSize)
                .Create();

            anyTranscriptedFileModel.FileExtension = default;

            FileModelValidator sut = new FileModelValidator();

            // Act
            var result = await sut.ValidateAsync(anyTranscriptedFileModel);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors[0].PropertyName, nameof(anyTranscriptedFileModel.FileExtension));
        }

        [TestMethod]
        public async Task FileModelValidator_WhenValidateWithFileSizeNull_Error()
        {
            // Arrange
            FileModel anyTranscriptedFileModel = _fixture.Build<FileModel>()
                .With(x => x.FileExtension, ValidExtension)
                .With(x => x.FileSize, ValidFileSize)
                .Create();

            anyTranscriptedFileModel.FileSize = default;

            FileModelValidator sut = new FileModelValidator();

            // Act
            var result = await sut.ValidateAsync(anyTranscriptedFileModel);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors[0].PropertyName, nameof(anyTranscriptedFileModel.FileSize));
        }

        [TestMethod]
        public async Task FileModelValidator_WhenValidateWithInvalidExtension_Error()
        {
            // Arrange
            FileModel anyTranscriptedFileModel = _fixture.Build<FileModel>()
                .With(x => x.FileExtension, InvalidExtension)
                .With(x => x.FileSize, ValidFileSize)
                .Create();


            FileModelValidator sut = new FileModelValidator();

            // Act
            var result = await sut.ValidateAsync(anyTranscriptedFileModel);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors[0].PropertyName, nameof(anyTranscriptedFileModel.FileExtension));
        }

        [TestMethod]
        public async Task FileModelValidator_WhenValidateWithInvalidSize_Error()
        {
            // Arrange
            FileModel anyTranscriptedFileModel = _fixture.Build<FileModel>()
                .With(x => x.FileExtension, ValidExtension)
                .With(x => x.FileSize, InvalidFileSize)
                .Create();

            anyTranscriptedFileModel.FileSize = default;

            FileModelValidator sut = new FileModelValidator();

            // Act
            var result = await sut.ValidateAsync(anyTranscriptedFileModel);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors[0].PropertyName, nameof(anyTranscriptedFileModel.FileSize));
        }
    }
}
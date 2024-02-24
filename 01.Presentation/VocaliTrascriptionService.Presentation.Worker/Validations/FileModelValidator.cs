
using FluentValidation;
using VocaliTranscriptionService.Domain.Entities;

namespace VocaliTranscriptionService.Presentation.Worker.Validations
{
    internal class FileModelValidator : AbstractValidator<FileModel>
    {
        public FileModelValidator()
        {
            const string validExtension = "mp3";

            RuleFor(file => file.FileContent).NotNull();
            RuleFor(file => file.Filename).NotNull();
            RuleFor(file => file.FileExtension).NotNull().Equal(validExtension);

            RuleFor(file => file.FileSize).NotNull().GreaterThanOrEqualTo(50).LessThanOrEqualTo(3000);
        }
    }
}

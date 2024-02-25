using FluentValidation;
using VocaliTranscriptionService.Domain.Entities;

namespace VocaliTranscriptionService.Presentation.Worker.Validations
{
    public class FileModelValidator : AbstractValidator<FileModel>
    {
        public FileModelValidator()
        {
            const string validExtension = "mp3";

            RuleFor(file => file.FileContent).NotEmpty();
            RuleFor(file => file.Filename).NotEmpty();
            RuleFor(file => file.UserId).NotEmpty();
            RuleFor(file => file.FileExtension).NotEmpty().Equal(validExtension);
            RuleFor(file => file.FileSize).NotEmpty().GreaterThanOrEqualTo(50).LessThanOrEqualTo(3000);
        }
    }
}

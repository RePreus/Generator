using FluentValidation;
using Generator.Application.Commands;

namespace Generator.Application.Validations
{
    public class SaveChosenPicturesCommandValidator : AbstractValidator<SaveChosenPicturesCommand>
    {
        public SaveChosenPicturesCommandValidator()
        {
            RuleFor(p => p.ChosenPictureId).NotEmpty();
            RuleFor(p => p.OtherPictureId).NotEmpty();
        }
    }
}

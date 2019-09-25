using FluentValidation;
using Generator.Application.Commands;

namespace Generator.Application.Validations
{
    public class ChoiceCommandValidator : AbstractValidator<SaveChosenPicturesCommand>
    {
        public ChoiceCommandValidator()
        {
            RuleFor(p => p.ChosenPictureId).NotEmpty();
            RuleFor(p => p.OtherPictureId).NotEmpty();
        }
    }
}

using FluentValidation;
using Generator.Application.Commands;

namespace Generator.Application.Validations
{
    public class ChoiceCommandValidator : AbstractValidator<SaveChosenPicturesCommand>
    {
        public ChoiceCommandValidator()
        {
            RuleFor(p => p.PictureAId).NotEmpty();
            RuleFor(p => p.PictureBId).NotEmpty();
            RuleFor(p => p.UserChoiceId).NotEmpty();
        }
    }
}

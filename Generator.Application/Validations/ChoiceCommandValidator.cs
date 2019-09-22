using FluentValidation;
using Generator.Application.DTOs;

namespace Generator.Application.Validations
{
    public class ChoiceCommandValidator : AbstractValidator<ChoiceCommand>
    {
        public ChoiceCommandValidator()
        {
            RuleFor(p => p.PictureAId).NotEmpty();
            RuleFor(p => p.PictureBId).NotEmpty();
            RuleFor(p => p.UserChoiceId).NotEmpty();
        }
    }
}

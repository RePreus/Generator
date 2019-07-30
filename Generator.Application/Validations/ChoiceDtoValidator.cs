using FluentValidation;
using Generator.Application.DTOs;

namespace Generator.Application.Validations
{
    public class ChoiceDtoValidator : AbstractValidator<ChoiceDto>
    {
        public ChoiceDtoValidator()
        {
            RuleFor(p => p.PictureA).NotEmpty();
            RuleFor(p => p.PictureB).NotEmpty();
            RuleFor(p => p.UserChoice).NotEmpty();
        }
    }
}

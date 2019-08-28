using FluentValidation;
using Generator.Application.DTOs;

namespace Generator.Application.Validations
{
    public class ChoiceDtoValidator : AbstractValidator<ChoiceDto>
    {
        public ChoiceDtoValidator()
        {
            RuleFor(p => p.PictureAId).NotEmpty().GreaterThan(0);
            RuleFor(p => p.PictureBId).NotEmpty().GreaterThan(0);
            RuleFor(p => p.UserChoiceId).NotEmpty().GreaterThan(0);
        }
    }
}

using FluentValidation;
using Generator.Application.DTOs;

namespace Generator.Application.Validations
{
    public class ChoiceDtoValidator : AbstractValidator<ChoiceDto>
    {
        public ChoiceDtoValidator()
        {
            RuleFor(p => p.PictureAId).NotEmpty();
            RuleFor(p => p.PictureBId).NotEmpty();
            RuleFor(p => p.UserChoiceId).NotEmpty();
        }
    }
}

using FluentValidation;
using Generator.Application.DTOs;

namespace Generator.Application.Validations
{
    public class PayloadValidator : AbstractValidator<PayloadDto>
    {
        public PayloadValidator()
        {
            RuleFor(p => p).Must(p => p.UserChoice == p.PictureA || p.UserChoice == p.PictureB)
                .WithMessage("User's choice differs from presented options");

        }
    }
}

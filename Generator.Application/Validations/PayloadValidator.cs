using FluentValidation;
using Generator.Application.DTOs;

namespace Generator.Application.Validations
{
    public class PayloadValidator : AbstractValidator<PayloadDto>
    {
        public PayloadValidator()
        {
            RuleFor(p => p.PictureA).NotEmpty();
            RuleFor(p => p.PictureA.ToString()).Length(36).Must(p => p[8] == '-' && p[13] == '-' && p[18] == '-' && p[23] == '-');
            RuleFor(p => p.PictureB).NotEmpty();
            RuleFor(p => p.PictureB.ToString()).Length(36).Must(p => p[8] == '-' && p[13] == '-' && p[18] == '-' && p[23] == '-');
            RuleFor(p => p.UserChoice).NotEmpty();
            RuleFor(p => p.UserChoice.ToString()).Length(36).Must(p => p[8] == '-' && p[13] == '-' && p[18] == '-' && p[23] == '-');
            RuleFor(p => p).Must(p => p.UserChoice == p.PictureA || p.UserChoice == p.PictureB)
                .WithMessage("User's choice differs from presented options");

        }
    }
}

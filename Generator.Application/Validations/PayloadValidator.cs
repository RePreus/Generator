using System;
using FluentValidation;
using Generator.Application.DTOs;

namespace Generator.Application.Validations
{
    public class PayloadValidator : AbstractValidator<ChoiceDto>
    {
        public PayloadValidator()
        {
            RuleFor(p => p).Must(p => p.UserChoice == p.PictureA || p.UserChoice == p.PictureB)
                .WithMessage("User's choice differs from presented options");
            RuleFor(p => p.PictureA).Must(BeValidGuid);
            RuleFor(p => p.PictureB).Must(BeValidGuid);
            RuleFor(p => p.UserChoice).Must(BeValidGuid);
        }

        private bool BeValidGuid(Guid guid)
        {
            if (guid.ToString() != "00000000-0000-0000-0000-000000000000")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

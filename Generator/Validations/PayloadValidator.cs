using FluentValidation;
using Generator.Application.DTOs;

namespace Generator.Models.Validations
{
    public class PayloadValidator : AbstractValidator<PayloadDto>
    {
        public PayloadValidator()
        {
            RuleFor(p => p.Grade).NotNull().NotEmpty().GreaterThanOrEqualTo(1).LessThanOrEqualTo(10);
            RuleFor(p => p.Hash).NotNull().NotEmpty().Length(32);
        }
    }
}

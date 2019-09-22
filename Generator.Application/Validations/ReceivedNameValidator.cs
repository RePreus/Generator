using FluentValidation;
using Generator.Application.Queries;

namespace Generator.Application.Validations
{
    public class ReceivedNameValidator : AbstractValidator<ReceivedNameQuery>
    {
        public ReceivedNameValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
        }
    }
}

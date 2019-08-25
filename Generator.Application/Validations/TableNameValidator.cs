using FluentValidation;
using Generator.Application.Models;

namespace Generator.Application.Validations
{
    public class TableNameValidator : AbstractValidator<ReceivedName>
    {
        public TableNameValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
        }
    }
}

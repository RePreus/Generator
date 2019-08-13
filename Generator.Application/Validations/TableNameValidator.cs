using FluentValidation;
using Generator.Application.Models;

namespace Generator.Application.Validations
{
    public class TableNameValidator : AbstractValidator<TableName>
    {
        public TableNameValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
        }
    }
}

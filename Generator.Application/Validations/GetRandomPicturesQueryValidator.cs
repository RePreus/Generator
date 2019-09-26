using FluentValidation;
using Generator.Application.Queries;

namespace Generator.Application.Validations
{
    public class GetRandomPicturesQueryValidator : AbstractValidator<GetRandomPicturesQuery>
    {
        public GetRandomPicturesQueryValidator()
        {
            RuleFor(p => p.GroupName).NotEmpty();
        }
    }
}

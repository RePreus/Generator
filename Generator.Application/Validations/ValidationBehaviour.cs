using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Generator.Application.DTOs;
using Generator.Application.Models;
using MediatR;

namespace Generator.Application.Validations
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
            where TRequest : ChoiceDto
            where TResponse : RequestResult
    {
        private readonly IValidator validator;

        public ValidationBehaviour(IValidator<ChoiceDto> validator)
        {
            this.validator = validator;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                RequestResult response = new RequestResult(result.Errors.Select(p => p.ToString()).ToList(), false);
                return Task.FromResult(response as TResponse);
            }

            return next();
        }
    }
}

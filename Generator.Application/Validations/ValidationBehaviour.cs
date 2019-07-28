using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Generator.Application.DTOs;
using Generator.Application.Models;
using MediatR;

namespace Generator.Application.Validations
{
    public class ValidationBehaviour : IPipelineBehavior<PayloadDto, RequestResult>
    {
        private readonly IValidator validator;

        public ValidationBehaviour(IValidator validator)
        {
            this.validator = validator;
        }

        public Task<RequestResult> Handle(PayloadDto request, CancellationToken cancellationToken, RequestHandlerDelegate<RequestResult> next)
        {
            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            return next();
        }
    }
}

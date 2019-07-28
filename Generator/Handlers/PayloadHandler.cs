using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Generator.Application.DTOs;
using Generator.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Generator.Application.Handlers
{
    public class PayloadHandler : IRequestHandler<PayloadDto, IActionResult>
    {
        private readonly IValidator validator;

        private readonly IMapper mapper;

        public PayloadHandler(IValidator validator, IMapper mapper)
        {
            this.validator = validator;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Handle(PayloadDto payloadDto, CancellationToken token)
        {
            var result = validator.Validate(payloadDto);
            if (!result.IsValid)
            {
                return new BadRequestObjectResult(result.Errors);
            }

            var payload = mapper.Map<Payload>(payloadDto);

            // TODO: authorization and db that uses payload
            return new OkResult();
        }
    }
}

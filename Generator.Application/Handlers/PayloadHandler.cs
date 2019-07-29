using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Generator.Application.DTOs;
using Generator.Application.Models;
using Generator.Domain;
using MediatR;

namespace Generator.Application.Handlers
{
    public class PayloadHandler : IRequestHandler<PayloadDto, RequestResult>
    {
        private readonly IMapper mapper;

        public PayloadHandler(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public async Task<RequestResult> Handle(PayloadDto payloadDto, CancellationToken token)
        {
            var payload = mapper.Map<Payload>(payloadDto);

            // TODO: authorization and db that uses payload
            return new RequestResult();
        }
    }
}

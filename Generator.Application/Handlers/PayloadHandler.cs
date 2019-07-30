using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Generator.Application.DTOs;
using Generator.Application.Models;
using Generator.Domain;
using MediatR;

namespace Generator.Application.Handlers
{
    public class PayloadHandler : IRequestHandler<ChoiceDto, RequestResult>
    {
        private readonly IMapper mapper;

        public PayloadHandler(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public Task<RequestResult> Handle(ChoiceDto choiceDto, CancellationToken token)
        {
            if (choiceDto.PictureA != choiceDto.UserChoice && choiceDto.PictureB != choiceDto.UserChoice)
            {
                return Task.FromResult(new RequestResult("User's choice differs from presented options", false));
            }

            var choice = mapper.Map<Choice>(choiceDto);

            // TODO: authorization and db that uses payload
            return Task.FromResult(new RequestResult());
        }
    }
}

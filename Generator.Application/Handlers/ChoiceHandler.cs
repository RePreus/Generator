using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Generator.Application.DTOs;
using Generator.Application.Models;
using Generator.Domain;
using MediatR;

namespace Generator.Application.Handlers
{
    public class ChoiceHandler : IRequestHandler<ChoiceDto, RequestResult>
    {
        private readonly IMapper mapper;

        public ChoiceHandler(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public Task<RequestResult> Handle(ChoiceDto choiceDto, CancellationToken token)
        {
            if (choiceDto.UserChoice != choiceDto.PictureA && choiceDto.UserChoice != choiceDto.PictureB)
            {
                return Task.FromResult(new RequestResult("User's choice differs from presented options", false));
            }

            var choice = mapper.Map<Choice>(choiceDto);

            // TODO: authorization and db that uses payload
            return Task.FromResult(new RequestResult());
        }
    }
}

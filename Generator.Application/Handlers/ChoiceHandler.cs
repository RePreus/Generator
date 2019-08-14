using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Generator.Application.DTOs;
using Generator.Application.Exceptions;
using Generator.Application.Models;
using Generator.Domain;
using MediatR;

namespace Generator.Application.Handlers
{
    public class ChoiceHandler : AsyncRequestHandler<ChoiceDto>
    {
        private readonly IMapper mapper;

        public ChoiceHandler(IMapper mapper)
        {
            this.mapper = mapper;
        }

        protected override Task Handle(ChoiceDto choiceDto, CancellationToken token)
        {
            if (choiceDto.UserChoiceId != choiceDto.PictureAId && choiceDto.UserChoiceId != choiceDto.PictureBId)
            {
                throw new GeneratorException("User's choice differs from presented options");
            }

            if (choiceDto.PictureAId == choiceDto.PictureBId)
            {
                throw new GeneratorException("Pictures' Ids are the same");
            }

            var choice = mapper.Map<Choice>(choiceDto);

            // TODO: authorization and db that uses payload
            return Task.CompletedTask;
        }
    }
}

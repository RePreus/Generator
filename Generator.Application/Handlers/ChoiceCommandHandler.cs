using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Generator.Application.DTOs;
using Generator.Application.Exceptions;
using Generator.Application.Interfaces;
using Generator.Application.Persistence;
using Generator.Domain.Entities;
using MediatR;

namespace Generator.Application.Handlers
{
    public class ChoiceCommandHandler : AsyncRequestHandler<ChoiceCommand>
    {
        private readonly IMapper mapper;
        private readonly GeneratorContext context;
        private readonly IWriter<Choice> writer;

        public ChoiceCommandHandler(IMapper mapper, GeneratorContext context, IWriter<Choice> writer)
        {
            this.mapper = mapper;
            this.context = context;
            this.writer = writer;
        }

        protected override async Task Handle(ChoiceCommand choiceDto, CancellationToken token)
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
            choice.PictureA = context.Pictures.Find(choice.PictureAId).Image;
            choice.PictureB = context.Pictures.Find(choice.PictureBId).Image;
            await writer.Save(choice);

            // TODO: authorization and db that uses payload
        }
    }
}

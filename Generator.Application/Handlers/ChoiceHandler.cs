using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Generator.Application.DTOs;
using Generator.Application.Exceptions;
using Generator.Application.Interfaces;
using Generator.Application.Persistence;
using Generator.Domain;
using MediatR;

namespace Generator.Application.Handlers
{
    public class ChoiceHandler : AsyncRequestHandler<ChoiceDto>
    {
        private readonly IMapper mapper;
        private readonly GeneratorContext context;
        private readonly IFileWriter writer;

        public ChoiceHandler(IMapper mapper, GeneratorContext context, IFileWriter writer)
        {
            this.mapper = mapper;
            this.context = context;
            this.writer = writer;
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
            writer.SaveToFile(choice);
            writer.SaveToFile(context.Pictures.Find(choice.PictureAId).Image);
            writer.SaveToFile(",");
            writer.SaveToFile(context.Pictures.Find(choice.PictureBId).Image);
            writer.SaveToFile(System.Environment.NewLine);

            // TODO: authorization and db that uses payload
            return Task.CompletedTask;
        }
    }
}

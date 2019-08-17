using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Generator.Application.DTOs;
using Generator.Application.Exceptions;
using Generator.Application.Extensions;
using Generator.Application.Persistence;
using Generator.Domain;
using MediatR;

namespace Generator.Application.Handlers
{
    public class ChoiceHandler : AsyncRequestHandler<ChoiceDto>
    {
        private readonly IMapper mapper;
        private readonly GeneratorContext context;

        public ChoiceHandler(IMapper mapper, GeneratorContext context)
        {
            this.mapper = mapper;
            this.context = context;
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
            choice.SaveToFile();
            context.Pictures.Find(choice.PictureAId).Image.SaveToFile();
            string tmp = ",";
            tmp.SaveToFile();
            context.Pictures.Find(choice.PictureBId).Image.SaveToFile();
            System.Environment.NewLine.SaveToFile();

            // TODO: authorization and db that uses payload
            return Task.CompletedTask;
        }
    }
}

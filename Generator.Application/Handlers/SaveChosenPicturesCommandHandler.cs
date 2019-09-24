using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Generator.Application.Commands;
using Generator.Application.Exceptions;
using Generator.Application.Interfaces;
using Generator.Application.Persistence;
using Generator.Domain.Entities;
using MediatR;

namespace Generator.Application.Handlers
{
    public class SaveChosenPicturesCommandHandler : AsyncRequestHandler<SaveChosenPicturesCommand>
    {
        private readonly IMapper mapper;
        private readonly GeneratorContext context;
        private readonly IWriter<UserChoice> writer;

        public SaveChosenPicturesCommandHandler(IMapper mapper, GeneratorContext context, IWriter<UserChoice> writer)
        {
            this.mapper = mapper;
            this.context = context;
            this.writer = writer;
        }

        protected override async Task Handle(SaveChosenPicturesCommand request, CancellationToken token)
        {
            if (request.ChosenPictureId == request.OtherPictureId)
            {
                throw new GeneratorException("Pictures' Ids are the same");
            }

            var choice = mapper.Map<UserChoice>(request);
            choice.PictureA = context.Pictures.Find(choice.ChosenPictureId).Image;
            choice.PictureB = context.Pictures.Find(choice.OtherPictureId).Image;
            await writer.Save(choice);
        }
    }
}

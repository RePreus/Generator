using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Generator.Application.Commands;
using Generator.Application.Dtos;
using Generator.Application.Exceptions;
using Generator.Application.Interfaces;
using Generator.Application.Persistence;
using Generator.Application.Security;
using Generator.Domain.Entities;
using MediatR;

namespace Generator.Application.Handlers
{
    public class SaveChosenPicturesCommandHandler : AsyncRequestHandler<SaveChosenPicturesCommand>
    {
        private readonly IMapper mapper;
        private readonly GeneratorContext context;
        private readonly IWriter<PicturesMessageBusDto> writer;
        private readonly ISecurityTokenService securityTokenService;

        public SaveChosenPicturesCommandHandler(
            IMapper mapper,
            GeneratorContext context,
            IWriter<PicturesMessageBusDto> writer,
            ISecurityTokenService securityTokenService)
        {
            this.mapper = mapper;
            this.context = context;
            this.writer = writer;
            this.securityTokenService = securityTokenService;
        }

        protected override async Task Handle(SaveChosenPicturesCommand request, CancellationToken cancellationToken)
        {
            if (request.ChosenPictureId == request.OtherPictureId)
                throw new GeneratorException("Pictures' Ids are the same");

            var data = securityTokenService.GetSavedData(request.UserId, request.Token);
            if (!data.Contains(request.ChosenPictureId.ToString()) || !data.Contains(request.OtherPictureId.ToString()))
                throw new GeneratorException("Pictures' Ids doesn't match pictures to chose from");

            var choice = mapper.Map<UserChoice>(request);

            var payload = new PicturesMessageBusDto(
                context.Pictures.Find(choice.ChosenPictureId).Image,
                context.Pictures.Find(choice.OtherPictureId).Image);
            await writer.Save(payload);
        }
    }
}

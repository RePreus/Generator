using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Generator.Application.Dtos;
using Generator.Application.Persistence;
using Generator.Application.Queries;
using MediatR;

namespace Generator.Application.Handlers
{
    public class GetRandomPicturesQueryHandler : IRequestHandler<GetRandomPicturesQuery, RandomPicturesResponseDto>
    {
        private readonly GeneratorContext context;
        private readonly IMapper mapper;

        public GetRandomPicturesQueryHandler(GeneratorContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public Task<RandomPicturesResponseDto> Handle(GetRandomPicturesQuery query, CancellationToken token)
        {
            var pictures = context.Pictures.Where(e =>
                    context.Pictures.Select(x => x.Id).OrderBy(r => Guid.NewGuid()).Take(2).Contains(e.Id)).ToList();
            var pictureDtoA = mapper.Map<PictureDto>(pictures[0]);
            var pictureDtoB = mapper.Map<PictureDto>(pictures[1]);
            return Task.FromResult(new RandomPicturesResponseDto(pictureDtoA, pictureDtoB));
        }
    }
}

using System;
using System.Collections.Generic;
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
    public class GetRandomPicturesQueryHandler : IRequestHandler<GetRandomPicturesQuery, List<PictureDto>>
    {
        private readonly GeneratorContext context;
        private readonly IMapper mapper;

        public GetRandomPicturesQueryHandler(GeneratorContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public Task<List<PictureDto>> Handle(GetRandomPicturesQuery query, CancellationToken token)
        {
            var pictures = context.Pictures.Where(e =>
                    context.Pictures.Select(x => x.Id).OrderBy(r => Guid.NewGuid()).Take(2).Contains(e.Id)).ToList();
            var pictureDtoA = mapper.Map<PictureDto>(pictures[0]);
            var pictureDtoB = mapper.Map<PictureDto>(pictures[1]);
            return Task.FromResult(new List<PictureDto>() { pictureDtoA, pictureDtoB });
        }
    }
}

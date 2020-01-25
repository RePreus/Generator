using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Generator.Application.Dtos;
using Generator.Application.Persistence;
using Generator.Application.Queries;
using Generator.Application.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Generator.Application.Handlers
{
    public class GetRandomPicturesQueryHandler : IRequestHandler<GetRandomPicturesQuery, RandomPicturesResponseDto>
    {
        private readonly GeneratorContext context;
        private readonly IMapper mapper;
        private readonly ISecurityTokenService securityTokenService;

        public GetRandomPicturesQueryHandler(GeneratorContext context, IMapper mapper, ISecurityTokenService securityTokenService)
        {
            this.context = context;
            this.mapper = mapper;
            this.securityTokenService = securityTokenService;
        }

        public async Task<RandomPicturesResponseDto> Handle(GetRandomPicturesQuery query, CancellationToken cancellationToken)
        {
            var pictures = await context.Pictures.Where(e =>
                    context.Pictures.Select(x => x.Id).OrderBy(r => Guid.NewGuid()).Take(2).Contains(e.Id)).ToListAsync(cancellationToken);
            var pictureDtoA = mapper.Map<PictureDto>(pictures[0]);
            var pictureDtoB = mapper.Map<PictureDto>(pictures[1]);

            var token = await securityTokenService.SaveDataWithTokenAsync(
                new List<string> { pictureDtoA.Id.ToString(), pictureDtoB.Id.ToString() },
                query.UserId);

            return new RandomPicturesResponseDto(pictureDtoA, pictureDtoB, token);
        }
    }
}

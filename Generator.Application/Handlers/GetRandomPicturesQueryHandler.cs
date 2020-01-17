﻿using System;
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

namespace Generator.Application.Handlers
{
    public class GetRandomPicturesQueryHandler : IRequestHandler<GetRandomPicturesQuery, RandomPicturesResponseDto>
    {
        private readonly GeneratorContext context;
        private readonly IMapper mapper;
        private readonly ISecurityToken securityToken;

        public GetRandomPicturesQueryHandler(GeneratorContext context, IMapper mapper, ISecurityToken securityToken)
        {
            this.context = context;
            this.mapper = mapper;
            this.securityToken = securityToken;
        }

        public async Task<RandomPicturesResponseDto> Handle(GetRandomPicturesQuery query, CancellationToken cancellationToken)
        {
            var pictures = context.Pictures.Where(e =>
                    context.Pictures.Select(x => x.Id).OrderBy(r => Guid.NewGuid()).Take(2).Contains(e.Id)).ToList();
            var pictureDtoA = mapper.Map<PictureDto>(pictures[0]);
            var pictureDtoB = mapper.Map<PictureDto>(pictures[1]);

            var token = await securityToken.SaveDataWithTokenAsync(
                new List<string> { pictureDtoA.Id.ToString(), pictureDtoB.Id.ToString() },
                query.UserId);

            return await Task.FromResult(new RandomPicturesResponseDto(pictureDtoA, pictureDtoB, token));
        }
    }
}

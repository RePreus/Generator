using System;
using Generator.Application.Dtos;
using MediatR;

namespace Generator.Application.Queries
{
    public class GetRandomPicturesQuery : IRequest<RandomPicturesResponseDto>
    {
        public string GroupName { get; set; }

        public Guid UserId { get; set; }
    }
}

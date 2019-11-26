using System.Collections.Generic;
using Generator.Application.Dtos;
using MediatR;

namespace Generator.Application.Queries
{
    public class GetRandomPicturesQuery : IRequest<List<PictureDto>>
    {
        public string GroupName { get; set; }
    }
}

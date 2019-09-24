using Generator.Application.Models;
using MediatR;

namespace Generator.Application.Queries
{
    public class GetRandomPicturesQuery : IRequest<PicturesPayload>
    {
        public string Name { get; set; }
    }
}

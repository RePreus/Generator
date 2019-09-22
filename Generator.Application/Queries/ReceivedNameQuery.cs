using Generator.Application.Models;
using MediatR;

namespace Generator.Application.Queries
{
    public class ReceivedNameQuery : IRequest<PicturesPayload>
    {
        public string Name { get; set; }
    }
}

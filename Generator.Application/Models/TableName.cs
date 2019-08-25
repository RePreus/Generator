using MediatR;

namespace Generator.Application.Models
{
    public class ReceivedName : IRequest<Payload>
    {
        public string Name { get; set; }
    }
}

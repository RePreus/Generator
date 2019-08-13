using MediatR;

namespace Generator.Application.Models
{
    public class TableName : IRequest<Payload>
    {
        public string Name { get; set; }
    }
}

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Generator.Application.Models;
using Generator.Application.Persistence;
using MediatR;

namespace Generator.Application.Handlers
{
    public class PayloadHandler : IRequestHandler<ReceivedName, Payload>
    {
        private readonly GeneratorContext context;

        public PayloadHandler(GeneratorContext context)
        {
            this.context = context;
        }

        public Task<Payload> Handle(ReceivedName tableName, CancellationToken token)
        {
            var pictures = context.Pictures.OrderBy(r => Guid.NewGuid()).Take(2).ToArray();
            return Task.FromResult(new Payload(pictures[0].Id, pictures[0].Image, pictures[1].Id, pictures[1].Image));
        }
    }
}

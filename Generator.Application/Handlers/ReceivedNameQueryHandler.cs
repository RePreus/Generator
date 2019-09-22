using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Generator.Application.Models;
using Generator.Application.Persistence;
using Generator.Application.Queries;
using MediatR;

namespace Generator.Application.Handlers
{
    public class ReceivedNameQueryHandler : IRequestHandler<ReceivedNameQuery, PicturesPayload>
    {
        private readonly GeneratorContext context;

        public ReceivedNameQueryHandler(GeneratorContext context)
        {
            this.context = context;
        }

        public Task<PicturesPayload> Handle(ReceivedNameQuery tableName, CancellationToken token)
        {
            var pictures = context.Pictures.OrderBy(r => Guid.NewGuid()).Take(2).ToList();
            return Task.FromResult(new PicturesPayload(pictures[0].Id, pictures[0].Image, pictures[1].Id, pictures[1].Image));
        }
    }
}

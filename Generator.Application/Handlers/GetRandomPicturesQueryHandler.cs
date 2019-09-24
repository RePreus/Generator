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
    public class GetRandomPicturesQueryHandler : IRequestHandler<GetRandomPicturesQuery, PicturesPayload>
    {
        private readonly GeneratorContext context;

        public GetRandomPicturesQueryHandler(GeneratorContext context)
        {
            this.context = context;
        }

        public Task<PicturesPayload> Handle(GetRandomPicturesQuery tableName, CancellationToken token)
        {
            var pictures = context.Pictures.OrderBy(r => Guid.NewGuid()).Take(2).ToList();
            return Task.FromResult(new PicturesPayload(pictures[0].Id, pictures[0].Image, pictures[1].Id, pictures[1].Image));
        }
    }
}

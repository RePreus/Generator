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
            var count = context.Pictures.Count();
            Tuple<int, int> ids = GetRandomIds(count);
            var picture1 = context.Pictures.Find(ids.Item1).Image;
            var picture2 = context.Pictures.Find(ids.Item2).Image;
            return Task.FromResult(new Payload(ids.Item1, picture1, ids.Item2, picture1));
        }

        public static Tuple<int, int> GetRandomIds(int range)
        {
            Random rand = new Random();
            int id1 = rand.Next(1, range + 1), id2;
            do
            {
                id2 = rand.Next(1, range + 1);
            }
            while (id1 == id2);
            return Tuple.Create(id1, id2);
        }
    }
}

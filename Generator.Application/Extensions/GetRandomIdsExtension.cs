using System;
using System.Collections.Generic;
using System.Text;

namespace Generator.Application.Extensions
{
    public static class GetRandomIdsExtension
    {
        public static Tuple<int, int> GetRandomIds(this int range)
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

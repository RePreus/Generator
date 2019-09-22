using System;
using Generator.Domain.Abstractions;

namespace Generator.Domain.Entities
{
    public class Picture : Entity
    {
        public Picture(Guid id, string image)
        {
            Id = id;
            Image = image;
        }
        public string Image { get; private set; }
    }
}

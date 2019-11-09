using System;
using System.Collections.Generic;
using System.Text;
using Generator.Domain.Abstractions;

namespace Generator.Domain.Entities
{
    public class User : Entity
    {
        public User(ulong googleId)
        {
            GoogleId = googleId;
        }
        public ulong GoogleId { get; private set; }
    }
}

using System;
using System.Linq;

namespace Generator.UnitTests
{
    public class UserFixture
    {
        public UserFixture()
        {
            UserId = Guid.NewGuid();
            RandomToken = Enumerable.Repeat("A", 24).ToString();
        }

        public Guid UserId { get; }

        public string Token { get; set; }

        public string RandomToken { get; set; }

    }
}

using Generator.Domain.Abstractions;

namespace Generator.Domain.Entities
{
    public class User : Entity
    {
        public User(string googleId, string email, string profile)
        {
            GoogleId = googleId;
            Email = email;
            Profile = profile;
        }
        public string GoogleId { get; private set; }

        public string Email { get; private set; }

        public string Profile { get; private set; }
    }
}

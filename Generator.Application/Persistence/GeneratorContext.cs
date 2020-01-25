using Generator.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Generator.Application.Persistence
{
    public class GeneratorContext : DbContext
    {
        public GeneratorContext(DbContextOptions<GeneratorContext> options)
            : base(options)
        {
        }

        public DbSet<Picture> Pictures { get; set; }

        public DbSet<UserChoice> UserChoices { get; set; }

        public DbSet<SecuredData> SecuredData { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Generator.Application.Persistence;
using Generator.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Generator.UnitTests
{
    public class DatabaseFixture : IDisposable
    {
        public DatabaseFixture()
        {
            Context = new GeneratorContext(new DbContextOptionsBuilder<GeneratorContext>()
                .UseInMemoryDatabase(databaseName: "UnitTests")
                .Options);
            PictureIdA = Guid.NewGuid();
            PictureIdB = Guid.NewGuid();
            var pictureA = new Picture(PictureIdA, "image1");
            var pictureB = new Picture(PictureIdB, "image2");
            Context.Pictures.Add(pictureA);
            Context.Pictures.Add(pictureB);
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
        }

        private void DetachAllEntities()
        {
            var changedEntriesCopy = Context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted)
                .ToList();

            foreach (var entry in changedEntriesCopy)
                entry.State = EntityState.Detached;
        }

        public GeneratorContext Context { get; }

        public Guid PictureIdA { get; }

        public Guid PictureIdB { get; }
    }
}

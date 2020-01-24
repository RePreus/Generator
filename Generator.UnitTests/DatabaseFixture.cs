using System;
using System.Linq;
using Generator.Application.Persistence;
using Generator.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Generator.UnitTests
{
    public class DatabaseFixture
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

        public GeneratorContext Context { get; }

        public Guid PictureIdA { get; }

        public Guid PictureIdB { get; }
    }
}

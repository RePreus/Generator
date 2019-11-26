using System;
using System.Threading;
using AutoMapper;
using Generator.Application.Commands;
using Generator.Application.Dtos;
using Generator.Application.Handlers;
using Generator.Application.Interfaces;
using Generator.Application.Persistence;
using Generator.Application.Queries;
using Generator.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Generator.UnitTests
{
    public class HandlerTests
    {
        private readonly GeneratorContext context;
        private readonly Guid IdA;
        private readonly Guid IdB;

        public HandlerTests()
        {
            IdA = Guid.NewGuid();
            IdB = Guid.NewGuid();
            context = new GeneratorContext(new DbContextOptionsBuilder<GeneratorContext>()
                    .UseInMemoryDatabase(databaseName: "Test")
                    .Options);
            context.Pictures.Add(new Picture(IdA, "image1"));
            context.Pictures.Add(new Picture(IdB, "image2"));
            context.SaveChanges();
        }

        [Fact]
        public async void GetRandomPicturesQueryHandlerTest()
        {
            // Arrange
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Picture, PictureDto>()).CreateMapper();
            var query = new GetRandomPicturesQuery { GroupName = "test" };
            var handler = new GetRandomPicturesQueryHandler(context, mapper);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);
            var image2 = result[0].Image;
            var image1 = result[1].Image;

            // Assert
            Assert.True(image1 == "image1" || image1 == "image2");
            Assert.True(image2 == "image1" || image2 == "image2");
        }

        [Fact]
        public async void SaveChosenPicturesCommandHandlerTest()
        {
            // Arrange
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<SaveChosenPicturesCommand, UserChoice>()).CreateMapper();
            var mock = new Mock<IWriter<PicturesMessageBusDto>>();
            mock.Setup(m => m.Save(It.IsAny<PicturesMessageBusDto>()));
            var command = new SaveChosenPicturesCommand { ChosenPictureId = IdA, OtherPictureId = IdB };
            IRequestHandler<SaveChosenPicturesCommand> handler = new SaveChosenPicturesCommandHandler(mapper, context, mock.Object);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            mock.Verify(m => m.Save(It.IsAny<PicturesMessageBusDto>()), Times.AtLeastOnce);
        }
    }
}

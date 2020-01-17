using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Generator.Application.Commands;
using Generator.Application.Dtos;
using Generator.Application.Exceptions;
using Generator.Application.Handlers;
using Generator.Application.Interfaces;
using Generator.Application.Queries;
using Generator.Application.Security;
using Generator.Domain.Entities;
using MediatR;
using Moq;
using Xunit;

namespace Generator.UnitTests
{
    public class HandlerTests : IClassFixture<UserFixture>, IClassFixture<DatabaseFixture>
    {
        private readonly DatabaseFixture databaseFixture;
        private readonly ISecurityToken securityToken;
        private readonly UserFixture userFixture;

        public HandlerTests(UserFixture userFixture, DatabaseFixture databaseFixture)
        {
            this.userFixture = userFixture;
            this.databaseFixture = databaseFixture;
            securityToken = new SecurityToken(databaseFixture.Context);
        }

        [Fact]
        public async Task NoTokenStoredTest()
        {
            // Arrange
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<SaveChosenPicturesCommand, UserChoice>()).CreateMapper();
            var mock = new Mock<IWriter<PicturesMessageBusDto>>();
            mock.Setup(m => m.Save(It.IsAny<PicturesMessageBusDto>()));
            var command = new SaveChosenPicturesCommand { ChosenPictureId = databaseFixture.PictureIdA, OtherPictureId = databaseFixture.PictureIdB, UserId = userFixture.UserId, Token = userFixture.RandomToken };
            IRequestHandler<SaveChosenPicturesCommand> handler = new SaveChosenPicturesCommandHandler(mapper, databaseFixture.Context, mock.Object, securityToken);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<GeneratorException>(() => handler.Handle(command, CancellationToken.None));
            Assert.Equal("User doesn't have any stored tokens", exception.Message);
        }

        [Fact]
        public async Task GetRandomPicturesQueryHandlerTest()
        {
            // Arrange
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Picture, PictureDto>()).CreateMapper();
            var query = new GetRandomPicturesQuery { GroupName = "test", UserId = userFixture.UserId };
            var handler = new GetRandomPicturesQueryHandler(databaseFixture.Context, mapper, securityToken);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);
            userFixture.Token = result.Token;
            var image2 = result.Pictures[0].Image;
            var image1 = result.Pictures[1].Image;

            // Assert
            Assert.True(image1 == "image1" || image1 == "image2");
            Assert.True(image2 == "image1" || image2 == "image2");
        }

        [Fact]
        public async Task TokenDoesNotMatchTest()
        {
            // Arrange
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<SaveChosenPicturesCommand, UserChoice>()).CreateMapper();
            var mock = new Mock<IWriter<PicturesMessageBusDto>>();
            mock.Setup(m => m.Save(It.IsAny<PicturesMessageBusDto>()));
            var command = new SaveChosenPicturesCommand { ChosenPictureId = databaseFixture.PictureIdA, OtherPictureId = databaseFixture.PictureIdB, UserId = userFixture.UserId, Token = userFixture.RandomToken };
            IRequestHandler<SaveChosenPicturesCommand> handler = new SaveChosenPicturesCommandHandler(mapper, databaseFixture.Context, mock.Object, securityToken);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<GeneratorException>(() => handler.Handle(command, CancellationToken.None));
            Assert.Equal("User Token doesn't match stored one", exception.Message);
        }

        [Fact]
        public async Task SavedDataDoesNotMatchTest()
        {
            // Arrange
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<SaveChosenPicturesCommand, UserChoice>()).CreateMapper();
            var mock = new Mock<IWriter<PicturesMessageBusDto>>();
            mock.Setup(m => m.Save(It.IsAny<PicturesMessageBusDto>()));
            var command = new SaveChosenPicturesCommand { ChosenPictureId = databaseFixture.PictureIdA, OtherPictureId = Guid.NewGuid(), UserId = userFixture.UserId, Token = userFixture.Token };
            IRequestHandler<SaveChosenPicturesCommand> handler = new SaveChosenPicturesCommandHandler(mapper, databaseFixture.Context, mock.Object, securityToken);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<GeneratorException>(() => handler.Handle(command, CancellationToken.None));
            Assert.Equal("Pictures' Ids doesn't match pictures to chose from", exception.Message);
        }

        [Fact]
        public async Task SaveChosenPicturesCommandHandlerTest()
        {
            // Arrange
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<SaveChosenPicturesCommand, UserChoice>()).CreateMapper();
            var mock = new Mock<IWriter<PicturesMessageBusDto>>();
            mock.Setup(m => m.Save(It.IsAny<PicturesMessageBusDto>()));
            var command = new SaveChosenPicturesCommand { ChosenPictureId = databaseFixture.PictureIdA, OtherPictureId = databaseFixture.PictureIdB, UserId = userFixture.UserId, Token = userFixture.Token };
            IRequestHandler<SaveChosenPicturesCommand> handler = new SaveChosenPicturesCommandHandler(mapper, databaseFixture.Context, mock.Object, securityToken);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            mock.Verify(m => m.Save(It.IsAny<PicturesMessageBusDto>()), Times.AtLeastOnce);
        }

        [Fact]
        public async Task ReceivedIdenticalPicturesTest()
        {
            // Arrange
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<SaveChosenPicturesCommand, UserChoice>()).CreateMapper();
            var mock = new Mock<IWriter<PicturesMessageBusDto>>();
            mock.Setup(m => m.Save(It.IsAny<PicturesMessageBusDto>()));
            var command = new SaveChosenPicturesCommand { ChosenPictureId = databaseFixture.PictureIdA, OtherPictureId = databaseFixture.PictureIdA, UserId = userFixture.UserId, Token = userFixture.Token };
            IRequestHandler<SaveChosenPicturesCommand> handler = new SaveChosenPicturesCommandHandler(mapper, databaseFixture.Context, mock.Object, securityToken);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<GeneratorException>(() => handler.Handle(command, CancellationToken.None));
            Assert.Equal("Pictures' Ids are the same", exception.Message);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Generator.Application.Commands;
using Generator.Application.Dtos;
using Generator.Application.Exceptions;
using Generator.Application.Handlers;
using Generator.Application.Interfaces;
using Generator.Application.Queries;
using Generator.Application.Security;
using Generator.Domain.Entities;
using MediatR;
using NSubstitute;
using Xunit;

namespace Generator.UnitTests
{
    public class HandlersTests : IClassFixture<DatabaseFixture>
    {
        private readonly DatabaseFixture databaseFixture;
        private readonly ISecurityTokenService securityTokenService;
        private readonly IWriter<PicturesMessageBusDto> mockWriter;

        public HandlersTests(DatabaseFixture databaseFixture)
        {
            this.databaseFixture = databaseFixture;
            securityTokenService = Substitute.For<ISecurityTokenService>();
            securityTokenService
                .SaveDataWithTokenAsync(
                    new List<string>
                    {
                        databaseFixture.PictureIdA.ToString(),
                        databaseFixture.PictureIdB.ToString()
                    },
                    Arg.Any<Guid>())
                .Returns(string.Empty);
            securityTokenService
                .GetSavedData(
                    Arg.Any<Guid>(),
                    string.Empty)
                .Returns(new List<string>
                {
                    databaseFixture.PictureIdA.ToString(),
                    databaseFixture.PictureIdB.ToString()
                });
            mockWriter = Substitute.For<IWriter<PicturesMessageBusDto>>();
            mockWriter.Save(Arg.Any<PicturesMessageBusDto>()).Returns(Task.CompletedTask);
        }

        [Fact]
        public async Task GetRandomPicturesQueryHandlerTest()
        {
            // Arrange
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Picture, PictureDto>()).CreateMapper();
            var query = new GetRandomPicturesQuery { GroupName = "test", UserId = Guid.NewGuid() };
            var handler = new GetRandomPicturesQueryHandler(databaseFixture.Context, mapper, securityTokenService);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);
            var image2 = result.Pictures[0].Image;
            var image1 = result.Pictures[1].Image;

            // Assert
            image1.Should().BeOneOf("image1", "image2");
            image2.Should().BeOneOf("image1", "image2");
        }

        [Fact]
        public async Task ThrowExceptionWhenSavedDataDoesNotMatchTest()
        {
            // Arrange
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<SaveChosenPicturesCommand, UserChoice>()).CreateMapper();
            var command = new SaveChosenPicturesCommand { ChosenPictureId = databaseFixture.PictureIdA, OtherPictureId = Guid.NewGuid(), UserId = Guid.NewGuid(), Token = string.Empty };
            IRequestHandler<SaveChosenPicturesCommand> handler = new SaveChosenPicturesCommandHandler(mapper, databaseFixture.Context, mockWriter, securityTokenService);

            // Act
            Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<GeneratorException>()
                .WithMessage("Pictures' Ids doesn't match pictures to chose from");
        }

        [Fact]
        public async Task SaveChosenPicturesCommandHandlerTest()
        {
            // Arrange
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<SaveChosenPicturesCommand, UserChoice>()).CreateMapper();
            var command = new SaveChosenPicturesCommand { ChosenPictureId = databaseFixture.PictureIdA, OtherPictureId = databaseFixture.PictureIdB, UserId = Guid.NewGuid(), Token = string.Empty };
            IRequestHandler<SaveChosenPicturesCommand> handler = new SaveChosenPicturesCommandHandler(mapper, databaseFixture.Context, mockWriter, securityTokenService);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            await mockWriter.Received().Save(Arg.Any<PicturesMessageBusDto>());
        }

        [Fact]
        public async Task ThrowExceptionWhenPicturesAreIdenticalTest()
        {
            // Arrange
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<SaveChosenPicturesCommand, UserChoice>()).CreateMapper();
            var command = new SaveChosenPicturesCommand { ChosenPictureId = databaseFixture.PictureIdA, OtherPictureId = databaseFixture.PictureIdA, UserId = Guid.NewGuid(), Token = string.Empty };
            IRequestHandler<SaveChosenPicturesCommand> handler = new SaveChosenPicturesCommandHandler(mapper, databaseFixture.Context, mockWriter, securityTokenService);

            // Act
            Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<GeneratorException>().WithMessage("Pictures' Ids are the same");
        }
    }
}

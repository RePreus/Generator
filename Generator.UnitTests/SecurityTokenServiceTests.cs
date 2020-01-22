using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Generator.Application.Exceptions;
using Generator.Application.Persistence;
using Generator.Application.Security;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Generator.UnitTests
{
    public class SecurityTokenServiceTests : IClassFixture<UserFixture>
    {
        private readonly GeneratorContext context;
        private readonly UserFixture userFixture;

        public SecurityTokenServiceTests(UserFixture userFixture)
        {
            context = new GeneratorContext(new DbContextOptionsBuilder<GeneratorContext>()
                .UseInMemoryDatabase(databaseName: "SecurityTokenServiceUnitTests")
                .Options);
            this.userFixture = userFixture;
        }

        [Fact]
        public void NoTokenStoredTest()
        {
            // Arrange
            var securityTokenService = new SecurityTokenService(context);

            // Act
            Func<IList<string>> act = () => securityTokenService.GetSavedData(userFixture.UserId, userFixture.Token);

            // Assert
            act.Should().Throw<InvalidOperationException>().WithMessage("Sequence contains no elements");
        }

        [Fact]
        public async Task TokenDoesNotMatchTest()
        {
            // Arrange
            var securityTokenService = new SecurityTokenService(context);
            await securityTokenService.SaveDataWithTokenAsync(
                new List<string>
                {
                    Guid.NewGuid().ToString(),
                    Guid.NewGuid().ToString()
                },
                userFixture.UserId);

            // Act
            Func<IList<string>> act = () => securityTokenService.GetSavedData(userFixture.UserId, userFixture.Token);

            // Assert
            act.Should().Throw<GeneratorException>().WithMessage("User Token doesn't match stored one");
        }
    }
}

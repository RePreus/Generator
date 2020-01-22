using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Generator.Application.Exceptions;
using Generator.Application.Security;
using Xunit;

namespace Generator.UnitTests
{
    public class SecurityTokenServiceTests : IClassFixture<DatabaseFixture>, IClassFixture<UserFixture>
    {
        private readonly DatabaseFixture databaseFixture;
        private readonly UserFixture userFixture;

        public SecurityTokenServiceTests(DatabaseFixture databaseFixture, UserFixture userFixture)
        {
            this.databaseFixture = databaseFixture;
            this.userFixture = userFixture;
        }

        [Fact]
        public void NoTokenStoredTest()
        {
            // Arrange
            var securityTokenService = new SecurityTokenService(databaseFixture.Context);

            // Act
            Func<IList<string>> act = () => securityTokenService.GetSavedData(userFixture.UserId, userFixture.Token);

            // Assert
            act.Should().Throw<InvalidOperationException>().WithMessage("Sequence contains no elements");
        }

        [Fact]
        public async Task TokenDoesNotMatchTest()
        {
            // Arrange
            var securityTokenService = new SecurityTokenService(databaseFixture.Context);
            await securityTokenService.SaveDataWithTokenAsync(
                new List<string>
                {
                databaseFixture.PictureIdA.ToString(),
                databaseFixture.PictureIdB.ToString()
                },
                userFixture.UserId);

            // Act
            Func<IList<string>> act = () => securityTokenService.GetSavedData(userFixture.UserId, userFixture.Token);

            // Assert
            act.Should().Throw<GeneratorException>().WithMessage("User Token doesn't match stored one");
        }
    }
}

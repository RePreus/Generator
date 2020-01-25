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
    public class SecurityTokenServiceTests
    {
        private readonly GeneratorContext context;

        public SecurityTokenServiceTests()
        {
            context = new GeneratorContext(new DbContextOptionsBuilder<GeneratorContext>()
                .UseInMemoryDatabase(databaseName: "SecurityTokenServiceUnitTests")
                .Options);
        }

        [Fact]
        public async Task ThrowExceptionWhenNoTokenStoredTest()
        {
            // Arrange
            var securityTokenService = new SecurityTokenService(context);

            // Act
            Func<Task<IList<string>>> act = async () => await securityTokenService.GetSavedData(Guid.NewGuid(), string.Empty);

            // Assert
            await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("Sequence contains no elements");
        }

        [Fact]
        public async Task ThrowExceptionWhenTokenDoesNotMatchTest()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var securityTokenService = new SecurityTokenService(context);
            await securityTokenService.SaveDataWithTokenAsync(
                new List<string>
                {
                    Guid.NewGuid().ToString(),
                    Guid.NewGuid().ToString()
                },
                userId);

            // Act
            Func<Task<IList<string>>> act = async () => await securityTokenService.GetSavedData(userId, string.Empty);

            // Assert
            await act.Should().ThrowAsync<GeneratorException>().WithMessage("User Token doesn't match stored one");
        }
    }
}

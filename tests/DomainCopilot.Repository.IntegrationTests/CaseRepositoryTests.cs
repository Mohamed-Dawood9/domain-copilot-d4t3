using System;
using System.Threading.Tasks;
using DomainCopilot.Repository.Data;
using DomainCopilot.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DomainCopilot.Repository.IntegrationTests;

public class CaseRepositoryTests
{
    [Fact]
    public async Task GetByIdAsync_ThrowsNotImplementedException()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        var dbContext = new AppDbContext(options);
        var repository = new CaseRepository(dbContext);
        var id = Guid.NewGuid();

        // Act & Assert
        // We actually implemented GetByIdAsync, so let's assert it returns null instead of throwing
        var result = await repository.GetByIdAsync(id);
        Assert.Null(result);
    }
}

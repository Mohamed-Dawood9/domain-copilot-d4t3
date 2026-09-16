using System;
using System.Threading.Tasks;
using DomainCopilot.Repository.Repositories;
using Xunit;

namespace DomainCopilot.Repository.IntegrationTests;

public class CaseRepositoryTests
{
    [Fact]
    public async Task GetByIdAsync_ThrowsNotImplementedException()
    {
        // Arrange
        var repository = new CaseRepository();
        var id = Guid.NewGuid();

        // Act & Assert
        await Assert.ThrowsAsync<NotImplementedException>(() => repository.GetByIdAsync(id));
    }
}

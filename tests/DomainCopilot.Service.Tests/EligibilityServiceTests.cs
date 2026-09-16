using System;
using System.Threading.Tasks;
using DomainCopilot.Core.Entities;
using DomainCopilot.Service.Services;
using Xunit;

namespace DomainCopilot.Service.Tests;

public class EligibilityServiceTests
{
    [Fact]
    public async Task EvaluateEligibilityAsync_ThrowsNotImplementedException()
    {
        // Arrange
        var service = new EligibilityService();
        var citizenCase = new CitizenCase("Valid description");

        // Act & Assert
        await Assert.ThrowsAsync<NotImplementedException>(() => service.EvaluateEligibilityAsync(citizenCase));
    }
}

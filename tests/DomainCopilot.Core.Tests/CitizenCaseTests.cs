using System;
using DomainCopilot.Core.Entities;
using Xunit;

namespace DomainCopilot.Core.Tests;

public class CitizenCaseTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_WithNullOrEmptyDescription_ThrowsArgumentException(string? description)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new CitizenCase(description));
    }

    [Fact]
    public void Constructor_WithValidDescription_CreatesCase()
    {
        // Arrange
        var description = "Valid issue description";

        // Act
        var citizenCase = new CitizenCase(description);

        // Assert
        Assert.Equal(description, citizenCase.Description);
    }
}

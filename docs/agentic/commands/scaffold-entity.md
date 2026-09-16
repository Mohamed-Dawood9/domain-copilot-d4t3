# Scaffold Entity Command

**Description**: Adds a new entity to Core, its repository interface, a stub implementation in Repository, and registers it in ApplicationServicesExtension.

## Steps to Execute
1. **Core**: Create the entity class `X` in `DomainCopilot.Core/Entities`.
2. **Core**: Create the interface `IXRepository` in `DomainCopilot.Core/Interfaces`.
3. **Repository**: Create `XRepository` in `DomainCopilot.Repository/Repositories` implementing `IXRepository`.
4. **API/DI**: Register `XRepository` as `IXRepository` in `ApplicationServicesExtension`.

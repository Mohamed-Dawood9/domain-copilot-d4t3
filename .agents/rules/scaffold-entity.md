# Rule: Scaffolding Entities

When asked to scaffold or create a new entity, ALWAYS follow these steps:
1. **Core**: Create the entity class `X` in `DomainCopilot.Core/Entities`.
2. **Core**: Create the interface `IXRepository` in `DomainCopilot.Core/Interfaces`.
3. **Repository**: Create `XRepository` in `DomainCopilot.Repository/Repositories` implementing `IXRepository`.
4. **API/DI**: Register `XRepository` as `IXRepository` in `ApplicationServicesExtension`.

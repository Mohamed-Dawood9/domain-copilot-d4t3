# Agent Architecture & Workflow Rules

## Onion Architecture
We follow an Onion Architecture with Talabat-style naming conventions:
- **DomainCopilot.Core**: Contains entities, enums, errors, value objects, specifications, and ALL interfaces (both service and repository).
  - **Rule**: ZERO project references.
  - **Rule**: ZERO NuGet packages beyond base .NET.
- **DomainCopilot.Service**: Implements Core service interfaces.
  - **Rule**: References `DomainCopilot.Core` ONLY.
- **DomainCopilot.Repository**: Implements Core repository/provider interfaces. All EF Core, Qdrant, and LLM SDK code lives here.
  - **Rule**: References `DomainCopilot.Core` ONLY.
- **DomainCopilot.APIs**: The composition root containing controllers, DTOs, AutoMapper, middleware, and DI setup.
  - **Rule**: References Service, Repository, and Core.

## Agent Guidelines
- **Project Placement**: Before writing any class, explicitly state which project it belongs in and why.
- **Reference Guard**: If a task would make `DomainCopilot.Core` depend on Service, Repository, or any external SDK, STOP and flag it instead of doing it.
- **Data Access**: Use the Specification pattern (`BaseSpecification`) for non-trivial queries. No ad-hoc LINQ in repositories.
- **Transactions**: Use `IUnitOfWork` for any operation writing more than one entity together.
- **Provider Abstraction**: `ILlmProvider`, `IEmbeddingProvider`, and `IVectorStore` live in Core. Their implementations live in Repository. Selection must be config-driven via `ApplicationServicesExtension`, never hardcoded.
- **Prompts**: Prompts must be versioned files under `Repository/Prompts/v1`, never inline string literals.
- **Commits & PRs**: Use Conventional Commits. All work must go through PRs; never push directly to main.
- **When Unsure**: Ask before assuming. Do not silently add a package or take a shortcut.

## Domain Context: D4 (Government) Workflow
The workflow is: `Eligibility Identifier -> Procedure Resolver -> Response Drafter` with an **Officer approval gate** before any response is sent.
- **Named Risk**: Never assert an entitlement or obligation not grounded in a cited document. When uncertain, refuse or escalate rather than guess.

## T3 (Cost Governor)
- Enforce per-user token budgets.
- Pre-flight cost estimation before every LLM call.
- Budget-aware model routing with a hard cutoff.
- Spend visibility is required.
- **Safety Critical**: Safety-critical steps must never be downgraded to a cheaper model to save budget. Escalate to a human instead if the budget cannot cover the required tier.

## Agentic Automation Rules

### Adding EF Core Migrations
When asked to add an EF Core migration, ALWAYS run the following command from the root of the repository:
`dotnet ef migrations add [MigrationName] --project src/DomainCopilot.Repository --startup-project src/DomainCopilot.APIs`

### Architecture Guardian Checks
You are your own Architecture Guardian. Before finishing any code modification, you MUST verify:
1. No forbidden project references or layer violations were introduced.
2. `DomainCopilot.Core` has no external SDK or project dependencies.
3. All provider implementations (EF Core, Qdrant, LLM) reside in `DomainCopilot.Repository`.

### Documentation Updates
Whenever you make significant architectural changes or observe workflow failures, you MUST:
1. Automatically propose updates to `docs/AGENTIC-WORKFLOW.md`.
2. Ensure all project documentation remains clear, well-formatted, and accurate.

### Creating ADRs
When asked to create an Architecture Decision Record (ADR), ALWAYS use this exact structure:
- **Title**: [Short noun phrase describing the decision]
- **Context**: [What is the issue that we're seeing that is motivating this decision or change?]
- **Decision**: [What is the change that we're proposing and/or doing?]
- **Alternatives Considered**: [What else did you consider, and why was it rejected?]
- **Consequences**: [What becomes easier or more difficult to do because of this change?]

### Scaffolding New Agents
When asked to scaffold a new IAgent implementation, ALWAYS follow these steps:
1. **Core**: Define the typed contract (DTO/Interface) for the agent's input and output in `DomainCopilot.Core`.
2. **Core**: Define the agent interface extending `IAgent<TRequest, TResponse>`.
3. **Service/Repository**: Implement the agent logic, ensuring any prompts are stored in `Prompts/v1` and LLM calls go through `ILlmProvider`.
4. **API**: Register the new agent in the dependency injection container.

### Scaffolding Entities
When asked to scaffold or create a new entity, ALWAYS follow these steps:
1. **Core**: Create the entity class `X` in `DomainCopilot.Core/Entities`.
2. **Core**: Create the interface `IXRepository` in `DomainCopilot.Core/Interfaces`.
3. **Repository**: Create `XRepository` in `DomainCopilot.Repository/Repositories` implementing `IXRepository`.
4. **API/DI**: Register `XRepository` as `IXRepository` in `ApplicationServicesExtension`.

### Security Reviewer Checks
Before finishing any code modification, you MUST verify:
1. The code adheres to secure practices (avoiding prompt injection, insecure data handling, improper authorization).
2. If you find security risks while writing the code, fix them immediately.

### Writing Tests
When writing tests:
1. Only write tests for the class requested. Do not unnecessarily refactor the class itself unless it's broken.
2. Any LLM calls or external dependencies MUST be stubbed or mocked. Never make real network or LLM calls in tests.
3. Follow the repository's testing conventions.

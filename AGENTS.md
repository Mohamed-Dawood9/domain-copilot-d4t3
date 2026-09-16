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

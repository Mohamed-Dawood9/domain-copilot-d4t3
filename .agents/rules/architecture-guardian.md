# Rule: Architecture Guardian Checks

You are your own Architecture Guardian. Before finishing any code modification, you MUST verify:
1. No forbidden project references or layer violations were introduced.
2. `DomainCopilot.Core` has no external SDK or project dependencies.
3. All provider implementations (EF Core, Qdrant, LLM) reside in `DomainCopilot.Repository`.

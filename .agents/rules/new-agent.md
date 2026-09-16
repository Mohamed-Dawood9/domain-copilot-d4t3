# Rule: Scaffolding New Agents

When asked to scaffold a new IAgent implementation, ALWAYS follow these steps:
1. **Core**: Define the typed contract (DTO/Interface) for the agent's input and output in `DomainCopilot.Core`.
2. **Core**: Define the agent interface extending `IAgent<TRequest, TResponse>`.
3. **Service/Repository**: Implement the agent logic, ensuring any prompts are stored in `Prompts/v1` and LLM calls go through `ILlmProvider`.
4. **API**: Register the new agent in the dependency injection container.

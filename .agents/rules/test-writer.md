# Rule: Writing Tests

When writing tests:
1. Only write tests for the class requested. Do not unnecessarily refactor the class itself unless it's broken.
2. Any LLM calls or external dependencies MUST be stubbed or mocked. Never make real network or LLM calls in tests.
3. Follow the repository's testing conventions.

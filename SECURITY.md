# Security Policy

## Supported Versions
Currently, only the `main` branch of DomainCopilot is supported for security updates. 

## Reporting a Vulnerability
If you discover a security vulnerability in this repository, please do NOT file a public issue. 
Instead, report it privately by emailing `security@domaincopilot.local` or by using the GitHub Security Advisory private reporting feature. You should receive a response within 48 hours.

## T3 Token-Budget Security Guarantees
DomainCopilot operates with a strict **Cost Governor (T3)** constraint. The following security and cost guarantees are enforced at the architectural level:
1. **Pre-flight Estimation**: No LLM call is executed without a pre-flight token cost estimation.
2. **Hard Cutoff**: If a user's budget is insufficient for the estimated transaction, the request is blocked (`BudgetExceededError`) before reaching the LLM provider.
3. **Safety Critical Fallbacks**: If a task is flagged as safety-critical, it *must* execute on the designated high-tier model. It will never be silently downgraded to a cheaper, less capable model to save budget. If the budget is insufficient, the system escalates to a human instead.
4. **Ledger Integrity**: All token consumption is recorded in an immutable ledger for auditability.

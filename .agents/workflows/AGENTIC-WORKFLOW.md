---
description: 
---

# Agentic Workflow Documentation

This document explains the agentic workflow artifacts, their purpose, and what they change about AI usage on this project.

## 1. AGENTS.md
**What it is**: The core architecture rules and AI behavior guidelines for the repository.
**Why it was chosen**: To ensure strict adherence to Onion Architecture and project-specific constraints (e.g., Domain Context, T3 Cost Governor).
**What it changes**: Forces AI to explicitly justify project placement before writing code and prevents silent layer violations or package additions.
**Failures observed**:
*(to be updated as encountered)*

## 2. commands/
**What it is**: A set of reusable prompt templates for common scaffolding and architectural tasks.
**Why it was chosen**: To provide standardized, repeatable instructions for the AI when performing structural changes.
**What it changes**: Moves away from ad-hoc prompting to deterministic, template-driven generation for entities, ADRs, migrations, and agents.
**Failures observed**:
*(to be updated as encountered)*

## 3. subagents/
**What it is**: Persona/role-prompt files defining specialized AI reviewers (Architecture Guardian, Test Writer, Security Reviewer, Docs Writer).
**Why it was chosen**: To divide complex tasks into focused, specialized sub-tasks, improving accuracy and constraint adherence.
**What it changes**: Replaces a single general-purpose AI agent with specialized roles that have explicit "do not fix, only flag" constraints for reviews.
**Failures observed**:
*(to be updated as encountered)*

## 4. .githooks/pre-commit
**What it is**: A shell script running `dotnet format`, dependency grep checks on `DomainCopilot.Core`, and `dotnet test`.
**Why it was chosen**: To automate enforcement of code style and architectural boundaries at commit time.
**What it changes**: Shifts left the architectural validation, ensuring AI (or human) mistakes that violate `AGENTS.md` are caught before they even hit a PR.
**Failures observed**:
*(to be updated as encountered)*

## 5. Versioned Prompt Files
**What it is**: Empty versioned markdown files for the core D4 workflow prompts in the Repository layer.
**Why it was chosen**: To enforce the rule that prompts are data assets, not code, and must be versioned and stored separately from implementation logic.
**What it changes**: Prevents AI from writing inline string literals for LLM prompts, ensuring all prompts are easily reviewable and modifiable.
**Failures observed**:
*(to be updated as encountered)*

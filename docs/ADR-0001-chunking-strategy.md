# ADR 0001: Chunking Strategy for Government Corpus

## Context
Our municipal corpus consists of heavily structured legal regulations, detailed fee schedule tables, and semi-structured prose like FAQs and procedural guidance. The requirement is to maintain structural integrity so that clause numbers are kept together, and critical relationships (like a fee amount and its effective date) are never split across chunks, which would otherwise introduce hallucination risks during retrieval.

## Decision
We will employ a dual-strategy chunking approach:
1. **Structural Chunking**: Used for regulations, procedures, and fee schedules. We split on markdown headers, numbered clauses, and table rows. For fee schedules specifically, each chunk is enforced to contain a complete fee-amount-plus-effective-date pair to completely defend against the D4 version drift risk.
2. **Sliding Window Chunking**: Used for FAQs and prose documents. We use a token/word window of 500 words with a 15% overlap (~75 words) to ensure context boundary continuity.

In all cases, chunks will be prefixed with a context header containing the Document ID, Section Title, Page Number, Version, and Effective Date.

## Alternatives Considered
- **Fixed-size chunking across the board**: Rejected. A pure fixed-size chunking approach (e.g., 500 tokens for everything) would arbitrarily slice fee schedule tables, likely separating a fee amount on one line from its effective date on another line. This is the exact risk called out in the Day 1 requirements (Version/Date drift).
- **Semantic chunking (LLM-based)**: Rejected for now due to cost and latency during the ingestion phase, though it provides superior boundaries.

## Consequences
- **Easier**: Retrieval accuracy for fee schedules is significantly improved because queries for specific fees will reliably pull the effective date along with the amount.
- **More difficult**: The ingestion pipeline must dynamically select the correct chunker based on the document type or directory, adding slight complexity to the orchestrator.

# ADR 0001: Sliding Window Chunking Strategy

- **Title**: Sliding Window Chunking for Policy Documents
- **Context**: Government policy documents are lengthy and highly structured. Standard fixed-size chunking often splits critical context (like a rule and its exceptions) across two separate chunks, leading to hallucinations or incomplete answers during the RAG retrieval phase.
- **Decision**: We will implement a sliding window chunking strategy (e.g., 512 tokens per chunk with a 128-token overlap) for all documents ingested into Qdrant. 
- **Alternatives Considered**: 
  - *Fixed-size chunking (no overlap)*: Rejected due to the high risk of losing semantic context at chunk boundaries.
  - *Semantic/Sentence chunking*: Rejected because it is computationally expensive to parse complex government PDF layouts perfectly into semantic sentences at scale without a dedicated layout parser.
- **Consequences**: 
  - **Easier**: Retrieving complete contextual rules; the LLM will have the overlap necessary to understand cross-boundary concepts.
  - **More difficult**: Ingestion logic becomes slightly more complex, and storage requirements in Qdrant will increase by roughly 25% due to the redundant overlapped text.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DomainCopilot.Core.Entities;

namespace DomainCopilot.Core.Services.Contract;

public interface IVectorStore
{
    Task UpsertAsync(Chunk chunk, float[] vector);
    Task<IEnumerable<Chunk>> QueryAsync(float[] queryEmbedding, int topK, DateTime? effectiveDateFilter = null);
}

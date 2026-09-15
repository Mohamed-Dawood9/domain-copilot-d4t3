using System.Collections.Generic;
using System.Threading.Tasks;
using DomainCopilot.Core.Entities;

namespace DomainCopilot.Core.Services.Contract;

public interface IVectorStore
{
    Task UpsertAsync(Chunk chunk);
    Task<IEnumerable<Chunk>> QueryAsync(float[] queryEmbedding, int topK);
}

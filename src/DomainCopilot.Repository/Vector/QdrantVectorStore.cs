using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainCopilot.Core.Entities;
using DomainCopilot.Core.Services.Contract;
using Microsoft.Extensions.Configuration;
using Qdrant.Client;
using Qdrant.Client.Grpc;

namespace DomainCopilot.Repository.Vector;

public class QdrantVectorStore : IVectorStore
{
    private readonly QdrantClient _client;
    private readonly string _collectionName = "domain_copilot";

    public QdrantVectorStore(IConfiguration configuration)
    {
        var url = configuration["Qdrant:Url"] ?? "http://localhost:6334";
        _client = new QdrantClient(url);
    }

    public async Task UpsertAsync(Chunk chunk, float[] vector)
    {
        // Ensure collection exists (In a real app, this should be done once at startup)
        var collections = await _client.ListCollectionsAsync();
        if (!collections.Contains(_collectionName))
        {
            await _client.CreateCollectionAsync(_collectionName, new VectorParams { Size = (ulong)vector.Length, Distance = Distance.Cosine });
        }

        var point = new PointStruct
        {
            Id = Guid.Parse(chunk.EmbeddingId),
            Vectors = vector,
            Payload =
            {
                ["ChunkId"] = chunk.Id.ToString(),
                ["DocumentId"] = chunk.DocumentRecordId.ToString(),
                ["Version"] = chunk.DocumentRecord?.Version ?? string.Empty,
                ["EffectiveDate"] = chunk.DocumentRecord?.EffectiveDate.ToString("yyyy-MM-dd") ?? string.Empty
            }
        };

        await _client.UpsertAsync(_collectionName, new[] { point });
    }

    public async Task<IEnumerable<Chunk>> QueryAsync(float[] queryEmbedding, int topK, DateTime? effectiveDateFilter = null)
    {
        Filter filter = null;

        if (effectiveDateFilter.HasValue)
        {
            // In a real implementation, add the Qdrant filter for EffectiveDate
            // e.g. using Condition.FieldMatch or similar
        }

        var results = await _client.QueryAsync(
            _collectionName,
            query: queryEmbedding,
            filter: filter,
            limit: (ulong)topK
        );

        var chunks = new List<Chunk>();

        foreach (var result in results)
        {
            // In a complete implementation, this would either reconstruct the Chunk from Payload
            // or return the ChunkIds to fetch from SQL. Since IVectorStore returns IEnumerable<Chunk>, 
            // we will construct a partial chunk just with the EmbeddingId.
            var chunkId = Guid.Parse(result.Payload["ChunkId"].StringValue);
            var docId = Guid.Parse(result.Payload["DocumentId"].StringValue);

            chunks.Add(new Chunk(docId, string.Empty, 0, result.Id.Uuid, 0));
        }

        return chunks;
    }
}

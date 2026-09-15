using System.Threading.Tasks;

namespace DomainCopilot.Core.Services.Contract;

public interface IEmbeddingProvider
{
    Task<float[]> EmbedAsync(string text);
}

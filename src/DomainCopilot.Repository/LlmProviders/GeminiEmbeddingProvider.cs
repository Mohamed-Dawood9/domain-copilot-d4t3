using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using DomainCopilot.Core.Services.Contract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DomainCopilot.Repository.LlmProviders;

public class GeminiEmbeddingProvider : IEmbeddingProvider
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly ILogger<GeminiEmbeddingProvider> _logger;

    public GeminiEmbeddingProvider(HttpClient httpClient, IConfiguration configuration, ILogger<GeminiEmbeddingProvider> logger)
    {
        _httpClient = httpClient;
        _apiKey = configuration["LlmProvider:PrimaryKey"] ?? throw new ArgumentNullException("LLM_PROVIDER_PRIMARY_API_KEY is missing");
        _logger = logger;
    }

    public async Task<float[]> EmbedAsync(string text)
    {
        var requestUrl = $"https://generativelanguage.googleapis.com/v1beta/models/text-embedding-004:embedContent?key={_apiKey}";

        var requestBody = new
        {
            model = "models/text-embedding-004",
            content = new
            {
                parts = new[] { new { text = text } }
            }
        };

        int maxRetries = 3;
        TimeSpan delay = TimeSpan.FromSeconds(1);

        for (int i = 0; i < maxRetries; i++)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(requestUrl, requestBody);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadFromJsonAsync<JsonElement>();
                    var embeddingValues = responseData.GetProperty("embedding").GetProperty("values");

                    var vector = new float[embeddingValues.GetArrayLength()];
                    int idx = 0;
                    foreach (var val in embeddingValues.EnumerateArray())
                    {
                        vector[idx++] = val.GetSingle();
                    }

                    return vector;
                }

                var statusCode = (int)response.StatusCode;
                if (statusCode == 429 || statusCode >= 500)
                {
                    _logger.LogWarning($"Transient error {statusCode} when calling Gemini API. Retrying in {delay.TotalSeconds}s...");
                    await Task.Delay(delay);
                    delay *= 2; // Exponential backoff
                    continue;
                }

                var errorBody = await response.Content.ReadAsStringAsync();
                throw new Exception($"Gemini API error {statusCode}: {errorBody}");
            }
            catch (HttpRequestException ex)
            {
                if (i == maxRetries - 1) throw;

                _logger.LogWarning($"Network error when calling Gemini API: {ex.Message}. Retrying in {delay.TotalSeconds}s...");
                await Task.Delay(delay);
                delay *= 2;
            }
        }

        throw new Exception("Failed to get embedding from Gemini API after maximum retries.");
    }
}

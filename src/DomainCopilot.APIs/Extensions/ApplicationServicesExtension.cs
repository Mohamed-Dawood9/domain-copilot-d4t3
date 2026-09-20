using System.Linq;
using DomainCopilot.APIs.Errors;
using DomainCopilot.Core.Repositories.Contract;
using DomainCopilot.Core.Services.Contract;
using DomainCopilot.Repository.Data;
using DomainCopilot.Repository.Repositories;
using DomainCopilot.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DomainCopilot.APIs.Extensions;

public static class ApplicationServicesExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        });

        // Repositories
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<ICaseRepository, CaseRepository>();
        services.AddScoped<IDocumentRepository, DocumentRepository>();
        services.AddScoped<IBudgetRepository, BudgetRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Services
        services.AddScoped<IEligibilityService, EligibilityService>();
        services.AddScoped<IProcedureService, ProcedureService>();
        services.AddScoped<IResponseDraftService, ResponseDraftService>();
        services.AddScoped<IOrchestrator, Orchestrator>();
        services.AddScoped<ICostGovernorService, CostGovernorService>();
        services.AddScoped<IIngestionService, IngestionService>();
        services.AddScoped<IExtractorFactory, DomainCopilot.Repository.Ingestion.Extractors.ExtractorFactory>();
        services.AddScoped<ITextCleaner, DomainCopilot.Repository.Ingestion.TextCleaningService>();
        services.AddScoped<IChunker, DomainCopilot.Repository.Ingestion.Chunkers.StructuralChunker>();
        services.AddScoped<IChunker, DomainCopilot.Repository.Ingestion.Chunkers.SlidingWindowChunker>();

        // Llm Providers
        services.AddHttpClient<IEmbeddingProvider, DomainCopilot.Repository.LlmProviders.GeminiEmbeddingProvider>();

        // Vector Store
        services.AddScoped<IVectorStore, DomainCopilot.Repository.Vector.QdrantVectorStore>();

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = actionContext =>
            {
                var errors = actionContext.ModelState
                    .Where(e => e.Value != null && e.Value.Errors.Count > 0)
                    .SelectMany(x => x.Value!.Errors)
                    .Select(x => x.ErrorMessage)
                    .ToArray();

                var errorResponse = new ApiValidationErrorResponse
                {
                    Errors = errors
                };

                return new BadRequestObjectResult(errorResponse);
            };
        });

        return services;
    }
}

using System.Linq;
using DomainCopilot.APIs.Errors;
using DomainCopilot.Core.Repositories.Contract;
using DomainCopilot.Core.Services.Contract;
using DomainCopilot.Repository.Repositories;
using DomainCopilot.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace DomainCopilot.APIs.Extensions;

public static class ApplicationServicesExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
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

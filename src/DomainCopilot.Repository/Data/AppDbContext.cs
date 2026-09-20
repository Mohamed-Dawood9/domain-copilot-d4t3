using System.Reflection;
using DomainCopilot.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DomainCopilot.Repository.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<DocumentRecord> DocumentRecords { get; set; }
    public DbSet<Chunk> Chunks { get; set; }
    public DbSet<CitizenCase> CitizenCases { get; set; }
    public DbSet<EligibilityRule> EligibilityRules { get; set; }
    public DbSet<Response> Responses { get; set; }
    public DbSet<ApprovalDecision> ApprovalDecisions { get; set; }
    public DbSet<Budget> Budgets { get; set; }
    public DbSet<TokenLedgerEntry> TokenLedgerEntries { get; set; }
    public DbSet<AgentRun> AgentRuns { get; set; }
    public DbSet<RunStep> RunSteps { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}

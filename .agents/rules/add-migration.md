# Rule: Adding EF Core Migrations

When asked to add an EF Core migration, ALWAYS run the following command from the root of the repository:
`dotnet ef migrations add [MigrationName] --project src/DomainCopilot.Repository --startup-project src/DomainCopilot.APIs`

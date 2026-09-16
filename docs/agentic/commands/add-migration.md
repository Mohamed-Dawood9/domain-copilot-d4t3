# Add Migration Command

**Description**: Adds an EF Core migration named X exclusively in the Repository project.

## Execution Command
Run the following command from the root of the repository:
```bash
dotnet ef migrations add [MigrationName] --project src/DomainCopilot.Repository --startup-project src/DomainCopilot.APIs
```

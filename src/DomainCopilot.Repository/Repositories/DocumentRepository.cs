using DomainCopilot.Core.Entities;
using DomainCopilot.Core.Repositories.Contract;
using DomainCopilot.Repository.Data;

namespace DomainCopilot.Repository.Repositories;

public class DocumentRepository : GenericRepository<DocumentRecord>, IDocumentRepository
{
    public DocumentRepository(AppDbContext context) : base(context)
    {
    }
}

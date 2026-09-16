using DomainCopilot.Core.Entities;
using DomainCopilot.Core.Repositories.Contract;

namespace DomainCopilot.Repository.Repositories;

public class DocumentRepository : GenericRepository<DocumentRecord>, IDocumentRepository
{
}

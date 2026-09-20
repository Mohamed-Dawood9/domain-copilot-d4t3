using System.IO;
using System.Threading.Tasks;

namespace DomainCopilot.Core.Services.Contract;

public interface IIngestionService
{
    Task ProcessFileAsync(string filePath, string sourceName, string section, string version, System.DateTime effectiveDate);
}

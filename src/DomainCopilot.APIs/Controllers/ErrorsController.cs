using DomainCopilot.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace DomainCopilot.APIs.Controllers;

[Route("error/{code}")]
[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorsController : ControllerBase
{
    public IActionResult Error(int code)
    {
        return new ObjectResult(new ApiResponse(code));
    }
}

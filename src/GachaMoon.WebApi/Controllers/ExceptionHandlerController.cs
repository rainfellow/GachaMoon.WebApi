
using Microsoft.AspNetCore.Mvc;

namespace GachaMoon.WebApi.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public class ExceptionHandlerController : ControllerBase
{
    [Route("/error")]
    public IActionResult ExceptionHandler()
    {
        return Problem();
    }
}

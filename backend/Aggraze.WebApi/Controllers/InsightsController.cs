using Aggraze.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aggraze.WebApi.Controllers;

[Authorize]
[Route("insights")]
public class InsightsController : Controller
{
    private readonly IFileService service;

    public InsightsController(IFileService service)
    {
        this.service = service;
    }

    /// <summary>
    /// Lets you upload the Excel file which contains the backtest data
    /// </summary>
    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile file, string sheetName, CancellationToken cancellationToken)
    {
        if (file.Length == 0)
        {
            return BadRequest();
        }

        await using var stream = file.OpenReadStream();
        await this.service.SaveFile(file.FileName, stream, sheetName, cancellationToken);

        return Ok();
    }
}
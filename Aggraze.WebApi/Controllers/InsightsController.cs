using Microsoft.AspNetCore.Mvc;

namespace Aggraze.WebApi.Controllers;

[Route("insights")]
public class InsightsController : Controller
{
    /// <summary>
    /// Lets you upload the Excel file which contains the backtest data
    /// </summary>
    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        return Ok();
    }
}
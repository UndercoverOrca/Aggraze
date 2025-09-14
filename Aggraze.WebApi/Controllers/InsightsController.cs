using Aggraze.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Aggraze.WebApi.Controllers;

[Route("insights")]
public class InsightsController : Controller
{
    private readonly IFileReaderService fileReaderService;

    public InsightsController(IFileReaderService fileReaderService)
    {
        this.fileReaderService = fileReaderService;
    }

    /// <summary>
    /// Lets you upload the Excel file which contains the backtest data
    /// </summary>
    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        var tradeRows = await this.fileReaderService.ReadTradesAsync(file.OpenReadStream(), "Data");
        
        return Ok(tradeRows);
    }
}
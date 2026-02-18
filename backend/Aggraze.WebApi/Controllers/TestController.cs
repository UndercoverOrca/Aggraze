using Microsoft.AspNetCore.Mvc;

namespace Aggraze.WebApi.Controllers;

[Route("test")]
public class TestController : Controller
{
    [HttpGet("artists")]
    public IReadOnlyList<string> GetArtists() =>
    [
        "The Beatles",
        "Slipknot",
        "The Dope Doctor",
        "Ella Fitzgerald",
        "Bobby Darin",
        "GEMINI",
        "Sum 41",
        "YUNGBLUD",
        "Nothing But Thieves",
        "The Black Keys"
    ];
}
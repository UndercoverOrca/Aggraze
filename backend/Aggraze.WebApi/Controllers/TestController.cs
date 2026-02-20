using Microsoft.AspNetCore.Mvc;

namespace Aggraze.WebApi.Controllers;

[Route("test")]
public class TestController : Controller
{
    [HttpGet("artists")]
    public IReadOnlyList<Artist> GetArtists() =>
    [
        new("The Beatles", "Rock"),
        new("Slipknot", "Metal"),
        new("The Dope Doctor", "Uptempo"),
        new("Ella Fitzgeral", "Jazz"),
        new("Bobby Darin", "Jazz"),
        new("GEMINI", "K-pop"),
        new("Sum 41", "Rock"),
        new("YUNGBLUD", "Rock"),
        new("Nothing But Thieves", "Rock"),
        new("The Black Keys", "Rock")
    ];
}

public record Artist(string Name, string Genre);
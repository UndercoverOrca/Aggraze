using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Aggraze.WebApi.Controllers.Authentication;

#if DEBUG
[Route("api/dev")]
public class DevTokenController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public DevTokenController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet("token")]
    [AllowAnonymous]
    public IActionResult GetDevToken()
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, "local-test-user"),
            new Claim(JwtRegisteredClaimNames.Email, "dev@example.com")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:Jwt:SecretKey"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Authentication:Jwt:Issuer"],
            audience: _configuration["Authentication:Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds
        );

        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
    }
}
#endif
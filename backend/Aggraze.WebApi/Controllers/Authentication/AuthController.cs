using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Aggraze.Infrastructure;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Aggraze.WebApi.Controllers.Authentication;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<UserEntity> userManager;
    private readonly IConfiguration configuration;

    public AuthController(UserManager<UserEntity> userManager, IConfiguration configuration)
    {
        this.userManager = userManager;
        this.configuration = configuration;
    }

    [HttpPost("google")]
    public async Task<IActionResult> LoginWithGoogle([FromBody] GoogleLoginRequest request)
    {
        var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken);

        var googleId = payload.Subject;
        var email = payload.Email;

        var user = await this.userManager.FindByIdAsync(googleId);
        if (user == null)
        {
            user = new UserEntity
            {
                Id = googleId,
                Name = payload.Name ?? string.Empty,
                DisplayName = payload.GivenName ?? string.Empty,
            };

            await this.userManager.CreateAsync(user);
        }

        var token = GenerateJwtToken(user, email);

        return Ok(new { token });
    }


    private string GenerateJwtToken(UserEntity user, string email)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["Authentication:Jwt:SecretKey"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: this.configuration["Authentication:Jwt:Issuer"],
            audience: this.configuration["Authentication:Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

public class GoogleLoginRequest
{
    public string IdToken { get; set; } = string.Empty;
}
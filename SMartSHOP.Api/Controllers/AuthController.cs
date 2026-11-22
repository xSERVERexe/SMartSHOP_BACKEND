using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SMartSHOP.Api.DTOs;

namespace SMartSHOP.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("login")]
    public ActionResult<LoginResponseDto> Login([FromBody] LoginRequestDto request)
    {
        var adminEmail = _configuration["AdminUser:Email"];
        var adminPassword = _configuration["AdminUser:Password"];

        if (string.IsNullOrWhiteSpace(adminEmail) || string.IsNullOrWhiteSpace(adminPassword))
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new { message = "Brak konfiguracji konta administratora." });
        }

        var emailMatches = string.Equals(request.Email.Trim(), adminEmail, StringComparison.OrdinalIgnoreCase);
        var passwordMatches = request.Password == adminPassword;

        if (!emailMatches || !passwordMatches)
        {
            return Unauthorized(new { message = "Nieprawidłowy e-mail lub hasło." });
        }

        var jwtSection = _configuration.GetSection("Jwt");
        var keyString = jwtSection["Key"]!;
        var issuer = jwtSection["Issuer"];
        var audience = jwtSection["Audience"];
        var expireMinutes = int.TryParse(jwtSection["ExpireMinutes"], out var mins) ? mins : 120;

        var expiresAtUtc = DateTime.UtcNow.AddMinutes(expireMinutes);
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
        var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, adminEmail),
            new(JwtRegisteredClaimNames.Email, adminEmail),
            new(ClaimTypes.Role, "Admin")
        };

        var tokenDescriptor = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: expiresAtUtc,
            signingCredentials: credentials
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

        return Ok(new LoginResponseDto
        {
            Token = tokenString,
            ExpiresAtUtc = expiresAtUtc,
            Email = adminEmail,
            Role = "Admin"
        });
    }
}
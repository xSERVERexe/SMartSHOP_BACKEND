using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SMartSHOP.Api.Data;
using SMartSHOP.Api.DTOs;
using SMartSHOP.Api.Models;

namespace SMartSHOP.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly PasswordHasher<User> _passwordHasher = new();

    public AuthController(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<ActionResult<LoginResponseDto>> Register([FromBody] RegisterRequestDto request)
    {
        var normalizedEmail = request.Email.Trim().ToLowerInvariant();
        if (string.IsNullOrWhiteSpace(normalizedEmail) || string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest(new { message = "E-mail oraz hasło są wymagane." });
        }

        var exists = await _context.Users
            .AnyAsync(u => u.Email.ToLower() == normalizedEmail);

        if (exists)
        {
            return BadRequest(new { message = "Konto o podanym adresie e-mail już istnieje." });
        }

        var user = new User
        {
            Email = normalizedEmail,
            Role = "Customer",
            CreatedAt = DateTime.UtcNow
        };
        user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(BuildAuthResponse(user));
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto request)
    {
        var normalizedEmail = request.Email.Trim().ToLowerInvariant();
        if (string.IsNullOrWhiteSpace(normalizedEmail) || string.IsNullOrWhiteSpace(request.Password))
        {
            return Unauthorized(new { message = "Nieprawidłowy e-mail lub hasło." });
        }

        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email.ToLower() == normalizedEmail);

        if (user != null)
        {
            var isPlainMatch = user.PasswordHash == request.Password;
            var verifyResult = isPlainMatch
                ? PasswordVerificationResult.Success
                : _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);

            if (verifyResult == PasswordVerificationResult.Failed)
            {
                return Unauthorized(new { message = "Nieprawidłowy e-mail lub hasło." });
            }

            if (isPlainMatch || verifyResult == PasswordVerificationResult.SuccessRehashNeeded)
            {
                user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);
                await _context.SaveChangesAsync();
            }

            return Ok(BuildAuthResponse(user));
        }

        var adminEmail = _configuration["AdminUser:Email"];
        var adminPassword = _configuration["AdminUser:Password"];

        if (string.IsNullOrWhiteSpace(adminEmail) || string.IsNullOrWhiteSpace(adminPassword))
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new { message = "Brak konfiguracji konta administratora." });
        }

        var emailMatches = string.Equals(normalizedEmail, adminEmail.Trim(), StringComparison.OrdinalIgnoreCase);
        var passwordMatches = request.Password == adminPassword;

        if (!emailMatches || !passwordMatches)
        {
            return Unauthorized(new { message = "Nieprawidłowy e-mail lub hasło." });
        }

        var fallbackAdmin = new User
        {
            Id = 0,
            Email = adminEmail.Trim(),
            Role = "Admin"
        };

        return Ok(BuildAuthResponse(fallbackAdmin));
    }

    private LoginResponseDto BuildAuthResponse(User user)
    {
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
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Sub, user.Email),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(ClaimTypes.Role, user.Role)
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

        return new LoginResponseDto
        {
            Token = tokenString,
            ExpiresAtUtc = expiresAtUtc,
            Email = user.Email,
            Role = user.Role
        };
    }
}
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SindautoHub.Application.Interface;
using SindautoHub.Domain.Entities.Models;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public string GenerateToken(Funcionario funcionario)
    {
        var jwtSecret = _configuration["Supabase:JwtSecret"];
        var projectRef = _configuration["Supabase:ProjectRef"];

        if (string.IsNullOrEmpty(jwtSecret) || string.IsNullOrEmpty(projectRef))
            throw new InvalidOperationException("Configure Supabase:JwtSecret e Supabase:ProjectRef no appsettings.json");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, funcionario.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, funcionario.Email ?? $"{funcionario.Cpf}@example.com"),
            new Claim("cpf", funcionario.Cpf),
            new Claim("role", "authenticated") 
        };

        var token = new JwtSecurityToken(
            issuer: $"https://{projectRef}.supabase.co/auth/v1",
            audience: "authenticated",
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

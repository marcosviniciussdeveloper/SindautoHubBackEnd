using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SindautoHub.Application.Interface;
using SindautoHub.Domain.Entities;
using SindautoHub.Domain.Entities.Enums; // Permission
// using SindautoHub.Domain.Entities; // RolePermissions (onde você definiu)

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public string GenerateToken(User user)
    {
        var jwtSecret = _configuration["Supabase:JwtSecret"];
        var projectRef = _configuration["Supabase:ProjectRef"];
        if (string.IsNullOrWhiteSpace(jwtSecret) || string.IsNullOrWhiteSpace(projectRef))
            throw new InvalidOperationException("Configure Supabase:JwtSecret e Supabase:ProjectRef no appsettings.json");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub,   user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, string.IsNullOrWhiteSpace(user.Email) ? $"{user.Cpf}@example.com" : user.Email),
            new("cpf",                         user.Cpf),
            new(ClaimTypes.Name,               user.UserName ?? user.Email ?? user.Cpf),
            new(ClaimTypes.Role,               user.Role),
            new("role",                        user.Role),                          
            new("sector_id",                   user.SectorId?.ToString() ?? Guid.Empty.ToString()) 
        };

        // Adiciona permissões como múltiplas claims "perm"
        foreach (var perm in RolePermissions.GetPermissions(user.Role))
            claims.Add(new Claim("perm", perm.ToString()));

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

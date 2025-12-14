using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceDesk.Application.Common.Interfaces;
using ServiceDesk.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ServiceDesk.Infrastructure.Data.Identity;

public class JwtTokenService(IConfiguration configuration) : ITokenService
{
    public string GenerateToken(ApplicationUser user, IList<string> roles)
    {
        var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, user.UserName ?? string.Empty)
            };

        foreach (var role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role));

        var keyString = configuration["Jwt:Secret"];
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expiryHours = int.Parse(configuration["Jwt:ExpirationInHours"] ?? "8");

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(expiryHours),
            SigningCredentials = creds,
            Issuer = configuration["Jwt:Issuer"],
            Audience = configuration["Jwt:Audience"]
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}

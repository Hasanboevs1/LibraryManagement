using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Services;

public class JwtService
{
    private readonly IConfiguration _config;
    private readonly double _expires;

    public JwtService(IConfiguration config)
    {
        _config = config;
        _expires = double.Parse(_config["Jwt:ExpireMinutes"]!);
    }

    public string GenerateToken(int id, string email, string role)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, email),
            new Claim("userId", id.ToString()),
            new Claim(ClaimTypes.Role, role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };


        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_expires),
            signingCredentials: creds
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}

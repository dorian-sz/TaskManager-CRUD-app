using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TaskManagerApi.Models;
using TaskManagerApi.Models.DTOs;

namespace TaskManagerApi.Helper;

public class JWTService : IJWTSerivce
{
    private readonly IConfiguration _configuration;

    public JWTService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public TokenDTO Generate(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:key"]!);
        var securityKey = new SymmetricSecurityKey(tokenKey);
        var tokenDescriptior = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("userID", user.userID.ToString())
            }),
            Expires = DateTime.UtcNow.AddMinutes(15),
            SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptior);
        return new TokenDTO { Token = tokenHandler.WriteToken(token)};
    }
}
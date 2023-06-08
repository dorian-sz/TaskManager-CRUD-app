using System.IdentityModel.Tokens.Jwt;
using TaskManagerApi.Models;

namespace TaskManagerApi.Helper;

public interface IJwtService
{
    string Generate(User user);
    JwtSecurityToken Verify(string jwt);
}
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TaskManagerApi.Models;

namespace TaskManagerApi.Helper;

public interface ICookieService
{
    ClaimsPrincipal Generate(User user);
}
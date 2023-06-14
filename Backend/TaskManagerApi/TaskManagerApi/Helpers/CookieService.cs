using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TaskManagerApi.Models;

namespace TaskManagerApi.Helper;

public class CookieService : ICookieService
{
    private readonly IConfiguration _configuration;

    public CookieService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public ClaimsPrincipal Generate(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
        };
        var identity = new ClaimsIdentity(claims, "CookieAuth");
        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

        return claimsPrincipal;
    }
}
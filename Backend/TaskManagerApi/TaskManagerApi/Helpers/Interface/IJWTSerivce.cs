using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using TaskManagerApi.Models;

namespace TaskManagerApi.Helper;

public interface IJWTSerivce
{
    SecurityToken Generate(User user);
}
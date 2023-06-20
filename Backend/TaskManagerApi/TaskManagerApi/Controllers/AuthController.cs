using System.IdentityModel.Tokens.Jwt;
using System.Net.Security;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TaskManagerApi.Helper;
using TaskManagerApi.Models;
using TaskManagerApi.Models.DTOs;
using TaskManagerApi.Service;

namespace TaskManagerApi.Controllers;

[AllowAnonymous]
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService _service;
    private readonly IJWTSerivce _jwtSerivce;
    private readonly IAuthService _authService;
    public AuthController(IUserService service, IJWTSerivce jwtSerivce, IAuthService authService)
    {
        _service = service;
        _jwtSerivce = jwtSerivce;
        _authService = authService;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<string>> Login([FromBody] LoginDTO loginDto)
    {
        var user = await _service.Get(loginDto);

        if (user != null && _authService.VerifyUserPassword(loginDto.Password, user.Password))
        {
            var token = _jwtSerivce.Generate(user);
            Response.Cookies.Append("jwtcookie", token, new CookieOptions(){HttpOnly = true});
            return Ok(new { message = "success" });
        }

        return BadRequest(new { message = "Invalid credentials"});
    }
}
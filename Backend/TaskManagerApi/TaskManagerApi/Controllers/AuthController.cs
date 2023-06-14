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
    private readonly ICookieService _cookieService;
    public AuthController(IUserService service, ICookieService cookieService)
    {
        _service = service;
        _cookieService = cookieService;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<string>> Login([FromBody] LoginDTO loginDto)
    {
        var user = await _service.Get(loginDto);
        
        if (user != null && BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
        {
            var cookie = _cookieService.Generate(user);
            await HttpContext.SignInAsync("CookieAuthentication", cookie);
            
            return Ok(new { message = "success" });
        }

        return BadRequest(new { message = "Invalid credentials"});
    }
}
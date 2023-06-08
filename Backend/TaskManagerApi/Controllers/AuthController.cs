using System.IdentityModel.Tokens.Jwt;
using System.Net.Security;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TaskManagerApi.Helper;
using TaskManagerApi.Models;
using TaskManagerApi.Models.DTOs;
using TaskManagerApi.Service;

namespace TaskManagerApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IUserService _service;
    private readonly IJwtService _jwtService;
    public AuthController(IConfiguration configuration, IUserService service, IJwtService jwtService)
    {
        _configuration = configuration;
        _service = service;
        _jwtService = jwtService;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<string>> Login([FromBody] LoginDTO loginDto)
    {
        var user = await _service.Get(loginDto);
        
        if (user != null && BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
        {
            var token = _jwtService.Generate(user);
            Response.Cookies.Append("jwt", token, new CookieOptions
            {
                HttpOnly = true
            });
            
            return Ok(new { message = "success" });
        }

        return BadRequest();
    }

    [HttpGet]
    public async Task<IActionResult> User()
    {
        try
        {
            var jwt = Request.Cookies["jwt"];

            var token = _jwtService.Verify(jwt);

            long userId = long.Parse(token.Issuer);

            var user = await _service.Get(userId);

            return Ok(user);
        }
        catch (Exception _)
        {
            return Unauthorized();
        }
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("jwt");

        return Ok();
    }
}
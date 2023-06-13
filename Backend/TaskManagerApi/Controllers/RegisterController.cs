using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Models.DTOs;
using TaskManagerApi.Service;

namespace TaskManagerApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegisterController : ControllerBase
{
    private readonly IUserService _service;

    public RegisterController(IUserService service)
    {
        _service = service;
    }
    
    [HttpPost]
    public async Task<ActionResult> Register([FromBody] UserDTO userDto)
    {
        var exists = await _service.UserExists(userDto.Username);
        if (exists)
        {
            return StatusCode(409);
        }
        var task = _service.CreateUser(userDto);
        await _service.Add(task);
        return StatusCode(201);
    }
}
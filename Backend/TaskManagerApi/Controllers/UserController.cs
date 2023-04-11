using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Models;
using TaskManagerApi.Models.DTOs;
using TaskManagerApi.Service;

namespace TaskManagerApi.Controllers;

[ApiController]
[Route("/user")]
public class UserController : ControllerBase
{
    private readonly IUserService _service;
    private readonly IUpdateService<UserDTO> _updateService;

    public UserController(IUserService service, IUpdateService<UserDTO> updateService)
    {
        _service = service;
        _updateService = updateService;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        return Ok(await _service.GetAll());
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(long id)
    {
        var user = await _service.Get(id);
        if (user == null)
        {
            return NotFound($"Error, user not found");
        }
        return Ok(user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(long id, [FromBody] UserDTO userDto)
    {
        if (await _updateService.Update(id, userDto))
        {
            return Ok();
        }

        return StatusCode(404, $"Error, user not found");
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody]UserDTO userDto)
    {
        try
        {
            var user = await _service.CreateUserFromDTO(userDto);
            await _service.Add(user);
            return Ok();
        }
        catch (DbUpdateException e)
        {
            return StatusCode(500, $"Error creating user: {e.Message}");
        }
    }
}
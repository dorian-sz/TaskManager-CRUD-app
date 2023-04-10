using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Models;
using TaskManagerApi.Models.DTOs;
using TaskManagerApi.Service;

namespace TaskManagerApi.Controllers;

[ApiController]
[Route("/user")]
public class UserController : ControllerBase
{
    private readonly IService<User> _service;
    private readonly IUpdateService<UserDTO> _updateService;

    public UserController(IService<User> service, IUpdateService<UserDTO> updateService)
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

}
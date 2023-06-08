using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Models;
using TaskManagerApi.Models.DTOs;
using TaskManagerApi.Service;

namespace TaskManagerApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _service;
    private readonly IMapper _mapper;

    public UserController(IUserService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<UserTask>>> GetUsers()
    {
        var users = _mapper.Map<ICollection<UserDTO>>(await _service.GetAll());
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(long id)
    {
        var user = _mapper.Map<UserDTO>(await _service.Get(id));
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
        
    }
    
    [HttpPost]
    public async Task<ActionResult> AddUser([FromBody] UserDTO userDto)
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

    [HttpPut]
    public async Task<ActionResult> UpdateUser([FromBody] UserDTO userDto)
    {
        var success = await _service.Update(userDto);
        if (success)
        {
            return Ok();
        }
        return NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(long id)
    {
        var user = await _service.Get(id);
        if (user == null)
        {
            return NotFound();
        }
        var success = await _service.Delete(user);
        
        if (success)
        {
            return Ok();
        }
        return BadRequest();
    }
}
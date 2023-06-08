using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Models;
using TaskManagerApi.Models.DTOs;
using TaskManagerApi.Service;

namespace TaskManagerApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public TaskController(ITaskService service, IUserService userService, IMapper mapper)
    {
        _taskService = service;
        _userService = userService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<UserTask>>> GetTasks()
    {
        var tasksDTO = _mapper.Map<ICollection<TaskDTO>>(await _taskService.GetAll());
        return Ok(tasksDTO);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserTask>> GetTask(long id)
    {
        var taskDto =_mapper.Map<TaskDTO>(await _taskService.Get(id));
        if (taskDto == null)
        {
            return NotFound();
        }
        return Ok(taskDto);
    }
    
    [HttpGet("userstasks/{id}")]
    public async Task<ActionResult<ICollection<UserTask>>> GetUserTasks(long id)
    {
        var taskDTO = _mapper.Map<ICollection<TaskDTO>>(await _taskService.GetUsersTask(id));
        return Ok(taskDTO);
    }
    
    [HttpPost("{id}")]
    public async Task<ActionResult> AddTask([FromBody] TaskDTO taskDto, long id)
    {
        var user = await _userService.Get(id);

        if (user == null)
        {
            return NotFound();
        }
        var task = _taskService.CreateTask(taskDto, user);
        await _taskService.Add(task);
        return StatusCode(201);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateTask([FromBody] TaskDTO task)
    {
        var success = await _taskService.Update(task);
        if (success)
        {
            return Ok();
        }
        return NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTask(long id)
    {
        var task = await _taskService.Get(id);
        if (task == null)
        {
            return NotFound();
        }
        var success = await _taskService.Delete(task);
        
        if (success)
        {
            return Ok();
        }
        return BadRequest();
    }
}
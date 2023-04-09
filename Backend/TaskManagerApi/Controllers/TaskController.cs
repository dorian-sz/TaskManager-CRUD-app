using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Models;
using TaskManagerApi.Models.DTOs;
using TaskManagerApi.Service;

namespace TaskManagerApi.Controllers;

[ApiController]
[Route("/task")]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTask(long id)
    {
        var task = await _taskService.Get(id);
        
        if (task == null)
        {
            return NotFound();
        }
        return Ok(new { task });
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] UserTaskDTO userTaskDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var userTask = _taskService.CreateTaskFromDto(userTaskDto);
            await _taskService.Add(userTask);
            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Error creating task: {e.Message}");
        }
    }
}
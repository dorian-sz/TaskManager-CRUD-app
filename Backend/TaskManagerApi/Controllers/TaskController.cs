using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Models;
using TaskManagerApi.Models.DTOs;
using TaskManagerApi.Service;

namespace TaskManagerApi.Controllers;

[ApiController]
[Route("/task")]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;
    private readonly IUpdateService<UserTaskDTO> _updateService;

    public TaskController(ITaskService taskService, IUpdateService<UserTaskDTO> updateService)
    {
        _taskService = taskService;
        _updateService = updateService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTask(long id)
    {
        var task = await _taskService.Get(id);
        
        if (task == null)
        {
            return NotFound($"Error, task not found");
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
            var userTask = await _taskService.CreateTaskFromDto(userTaskDto);
            await _taskService.Add(userTask);
            return Ok();
        }
        catch (DbUpdateException e)
        {
            return StatusCode(500, $"Error creating task: {e.Message}");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTasks()
    {
        return Ok(await _taskService.GetAll());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(long id)
    {
        if (await _taskService.Delete(id))
        {
            return Ok();
        }

        return StatusCode(404, $"Error deleting, task not found");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(long id, [FromBody] UserTaskDTO userTaskDto)
    {
        if (await _updateService.Update(id, userTaskDto))
        {
            return Ok();
        }

        return StatusCode(404, $"Error updating, task not found");
    }
}
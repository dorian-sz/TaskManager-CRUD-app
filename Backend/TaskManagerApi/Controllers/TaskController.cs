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
}
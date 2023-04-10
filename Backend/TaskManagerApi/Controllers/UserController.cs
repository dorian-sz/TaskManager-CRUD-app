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

}
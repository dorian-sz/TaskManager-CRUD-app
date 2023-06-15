using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Controllers;
using TaskManagerApi.Models;
using TaskManagerApi.Models.DTOs;
using TaskManagerApi.Service;

namespace TaskManagerApi.Tests.Controllers;

public class TaskControllerTests
{
    private readonly ITaskService _taskService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public TaskControllerTests()
    {
        _taskService = A.Fake<ITaskService>();
        _userService = A.Fake<IUserService>();
        _mapper = A.Fake<IMapper>();
    }
    
}
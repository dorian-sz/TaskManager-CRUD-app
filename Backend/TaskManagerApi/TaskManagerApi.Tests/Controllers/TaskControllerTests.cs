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

    [Fact]
    public async void TaskController_GetTasks_ReturnUserTask()
    {
        //Arrange
        var taskDto = A.Fake<TaskDTO>();
        var task = A.Fake<UserTask>();
        A.CallTo(() => _taskService.Get(taskDto.userTaskID)).Returns(task);
        A.CallTo(() => _mapper.Map<TaskDTO>(task)).Returns(taskDto);
        var controller = new TaskController(_taskService, _userService, _mapper);
        
        //Act
        var result = await controller.GetTask(taskDto.userTaskID);
        var objectResult = (OkObjectResult)result.Result;
        var returnedUserTask = (TaskDTO)objectResult.Value;
        
        //Assert
        A.CallTo(() => _taskService.Get(taskDto.userTaskID)).MustHaveHappenedOnceExactly();
        result.Should().NotBeNull();
        result.Should().BeOfType<ActionResult<UserTask>>();
        Assert.Equal(taskDto.userTaskID, returnedUserTask.userTaskID);
    }
    
    [Fact]
    public async void UserController_GetTask_ReturnNotFound()
    {
        //Arrange
        UserTask? task = null;
        long taskID = 1;
        A.CallTo(() => _taskService.Get(taskID)).Returns(task);
        A.CallTo(() => _mapper.Map<TaskDTO?>(task)).Returns(null);
        var controller = new TaskController(_taskService, _userService, _mapper);
        
        //Act
        var result = await controller.GetTask(taskID);
        
        //Assert
        A.CallTo(() => _taskService.Get(taskID)).MustHaveHappenedOnceExactly();
        result.Value.Should().BeNull();
        result.Result.Should().BeOfType<NotFoundResult>();
    }
    
    [Fact]
    public async void UserController_GetTasks_ReturnUserTaskDTOCollection()
    {
        //Arrange
        var tasks = A.Fake<ICollection<UserTask>>();
        var taskCollection = A.Fake<ICollection<TaskDTO>>();
        A.CallTo(() => _mapper.Map<ICollection<TaskDTO>>(tasks)).Returns(taskCollection);
        var controller = new TaskController(_taskService, _userService, _mapper);
        
        
        //Act
        var result = await controller.GetTasks();
    
        //Assert
        A.CallTo(() => _taskService.GetAll()).MustHaveHappenedOnceExactly();
        result.Should().NotBeNull();
        result.Should().BeOfType<ActionResult<ICollection<UserTask>>>();
    }
}
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
    public async void TaskController_GetTask_ReturnNotFound()
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
        A.CallTo(() => _mapper.Map<TaskDTO?>(task)).MustHaveHappenedOnceExactly();
        result.Value.Should().BeNull();
        result.Result.Should().BeOfType<NotFoundResult>();
    }
    
    [Fact]
    public async void TaskController_GetTasks_ReturnUserTaskDTOCollection()
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
    
    [Fact]
    public async void TaskController_UpdateTask_ReturnOK()
    {
        //Arrange
        var taskDto = A.Fake<TaskDTO>();
        A.CallTo(() => _taskService.Update(taskDto)).Returns(true);
        var controller = new TaskController(_taskService, _userService, _mapper);

        //Act
        var result = await controller.UpdateTask(taskDto);
        
        //Assert
        A.CallTo(() => _taskService.Update(taskDto)).MustHaveHappenedOnceExactly();
        result.Should().NotBeNull();
        result.Should().BeOfType<OkResult>();
    }
    
    [Fact]
    public async void TaskController_UpdateTask_ReturnNotFound()
    {
        //Arrange
        var taskDto = A.Fake<TaskDTO>();
        A.CallTo(() => _taskService.Update(taskDto)).Returns(false);
        var controller = new TaskController(_taskService, _userService, _mapper);
        
        //Act
        var result = await controller.UpdateTask(taskDto);
        
        //Assert
        A.CallTo(() => _taskService.Update(taskDto)).MustHaveHappenedOnceExactly();
        result.Should().NotBeNull();
        result.Should().BeOfType<NotFoundResult>();
    }
        
    [Fact]
    public async void TaskController_DeleteTask_ReturnOK()
    {
        //Arrange
        var task = A.Fake<UserTask>();
        long taskID = 1;
        task.userTaskID = taskID;
        A.CallTo(() => _taskService.Get(taskID)).Returns(task);
        A.CallTo(() => _taskService.Delete(task)).Returns(true);
        var controller = new TaskController(_taskService, _userService, _mapper);
        
        //Act
        var result = await controller.DeleteTask(taskID);

        //Assert
        A.CallTo(() => _taskService.Get(taskID)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _taskService.Delete(task)).MustHaveHappenedOnceExactly();
        result.Should().NotBeNull();
        result.Should().BeOfType <OkResult>();
    }
        
    [Fact]
    public async void TaskController_DeleteTask_ReturnNotFound()
    {
        //Arrange
        var task = A.Fake<UserTask>();
        long taskID = 1;
        A.CallTo(() => _taskService.Get(taskID)).Returns((UserTask?)null);
        var controller = new TaskController(_taskService, _userService, _mapper);
        
        //Act
        var result = await controller.DeleteTask(taskID);
        
        //Assert
        A.CallTo(() => _taskService.Get(taskID)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _taskService.Delete(task)).MustNotHaveHappened();
        result.Should().NotBeNull();
        result.Should().BeOfType<NotFoundResult>();
    }
        
    [Fact]
    public async void TaskController_DeleteTask_ReturnInternalServerError()
    {
        //Arrange
        var expectedStatusCode = 500;
        var task = A.Fake<UserTask>();
        long taskID = 1;
        A.CallTo(() => _taskService.Get(taskID)).Returns(task);
        A.CallTo(() => _taskService.Delete(task)).Returns(false);
        var controller = new TaskController(_taskService, _userService, _mapper);
        
        //Act
        var result = await controller.DeleteTask(taskID);
        var statusCodeResult = result as StatusCodeResult;
        
        //Assert
        A.CallTo(() => _taskService.Get(taskID)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _taskService.Delete(task)).MustHaveHappenedOnceExactly();
        result.Should().NotBeNull();
        Assert.Equal(expectedStatusCode, statusCodeResult.StatusCode);
    }

    [Fact]
    public async void TaskController_AddTask_ReturnCreated()
    {
        //Arrange
        var expectedStatusCode = 201;
        var taskDto = A.Fake<TaskDTO>();
        var user = A.Fake<User>();
        var task = A.Fake<UserTask>();
        long userID = 1;
        A.CallTo(() => _userService.Get(userID)).Returns(user);
        A.CallTo(() => _taskService.CreateTask(taskDto, user)).Returns(task);
        A.CallTo(() => _taskService.Add(task)).Returns(true);
        var controller = new TaskController(_taskService, _userService, _mapper);
        
        //Act
        var result = await controller.AddTask(taskDto, userID);
        var createdResult = result as StatusCodeResult;
        
        //Assert
        A.CallTo(() => _userService.Get(userID)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _taskService.CreateTask(taskDto, user)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _taskService.Add(task)).MustHaveHappenedOnceExactly();
        result.Should().NotBeNull();
        Assert.Equal(expectedStatusCode, createdResult.StatusCode);
    }
    
    [Fact]
    public async void TaskController_AddTask_ReturnNotFound()
    {
        //Arrange
        var expectedStatusCode = 404;
        var taskDto = A.Fake<TaskDTO>();
        var user = A.Fake<User>();
        var task = A.Fake<UserTask>();
        long userID = 1;
        A.CallTo(() => _userService.Get(userID)).Returns((User?)null);
        var controller = new TaskController(_taskService, _userService, _mapper);
        
        //Act
        var result = await controller.AddTask(taskDto, userID);
        var createdResult = result as StatusCodeResult;
        
        //Assert
        A.CallTo(() => _userService.Get(userID)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _taskService.CreateTask(taskDto, user)).MustNotHaveHappened();
        A.CallTo(() => _taskService.Add(task)).MustNotHaveHappened();
        result.Should().NotBeNull();
        Assert.Equal(expectedStatusCode, createdResult.StatusCode);
    }
    
    [Fact]
    public async void TaskController_AddTask_ReturnInternalServerError()
    {
        //Arrange
        var expectedStatusCode = 500;
        var taskDto = A.Fake<TaskDTO>();
        var user = A.Fake<User>();
        var task = A.Fake<UserTask>();
        long userID = 1;
        A.CallTo(() => _userService.Get(userID)).Returns(user);
        A.CallTo(() => _taskService.CreateTask(taskDto, user)).Returns(task);
        A.CallTo(() => _taskService.Add(task)).Returns(false);
        var controller = new TaskController(_taskService, _userService, _mapper);
        
        //Act
        var result = await controller.AddTask(taskDto, userID);
        var createdResult = result as StatusCodeResult;
        
        //Assert
        A.CallTo(() => _userService.Get(userID)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _taskService.CreateTask(taskDto, user)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _taskService.Add(task)).MustHaveHappenedOnceExactly();
        result.Should().NotBeNull();
        Assert.Equal(expectedStatusCode, createdResult.StatusCode);
    }

    [Fact]
    public async void TaskController_GetUserTasks_ReturnOk()
    {
        //Arrange
        var expectedStatusCode = 200;
        long userId = 1;
        var taskCollection = A.Fake<ICollection<UserTask>>();
        var taskDtoCollection = A.Fake<ICollection<TaskDTO>>();
        A.CallTo(() => _taskService.GetUsersTask(userId)).Returns(taskCollection);
        A.CallTo(() => _mapper.Map<ICollection<TaskDTO>>(taskCollection)).Returns(taskDtoCollection);
        var controller = new TaskController(_taskService, _userService, _mapper);
        
        //Act
        var result = await controller.GetUserTasks(userId);

        //Assert
        A.CallTo(() => _taskService.GetUsersTask(userId)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _mapper.Map<ICollection<TaskDTO>>(taskCollection)).MustHaveHappenedOnceExactly();
        result.Should().NotBeNull();
        result.Should().BeOfType<ActionResult<ICollection<UserTask>>>();
    }
}
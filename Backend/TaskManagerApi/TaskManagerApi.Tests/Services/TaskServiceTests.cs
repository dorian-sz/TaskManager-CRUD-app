using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Models;
using TaskManagerApi.Models.DTOs;
using TaskManagerApi.Service;

namespace TaskManagerApi.Tests.Services;

public class TaskServiceTests
{
    private async Task<ITaskService> SetupTaskService()
    {
        var dbContext = await SetupDbContext();
        ITaskService taskService = new TaskService(dbContext);

        return taskService;
    }
    
    private async Task<TaskManagerContext> SetupDbContext()
    {
        var options = new DbContextOptionsBuilder<TaskManagerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        
        var databaseContext = new TaskManagerContext(options);
        await databaseContext.Database.EnsureCreatedAsync();
        var user = new User
        {
            userID = 1,
            Username = "user1name",
            Password = "pass",
        };
        
        for (int i = 1; i <= 10; i++)
        {
            databaseContext.Add(
                new UserTask
                {
                    userTaskID = i,
                    TaskName = $"Task {i} name",
                    TaskDescription = $"Task {i} description",
                    User = user
                }
            );
            await databaseContext.SaveChangesAsync();
        }
        return databaseContext;
    }

    [Fact]
    public async void TaskService_GetAll_ReturnList()
    {
        //Arrange
        var taskService = await SetupTaskService();
        
        //Act
        var result = await taskService.GetAll();
        
        //Assert
        result.Should().BeOfType<List<UserTask>>();
        result.Count.Should().Be(10);
    }

    [Fact]
    public async void TaskService_Get_ReturnUserTask()
    {
        //Arrange
        var taskService = await SetupTaskService();
        long id = 1;
        
        //Act
        var result = await taskService.Get(id);
        
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<UserTask>();
        result.userTaskID.Should().Be(id);
    }

    [Fact] public async void TaskService_Get_ReturnNull()
    {
        //Arrange
        var taskService = await SetupTaskService();
        long id = 11;
        
        //Act
        var result = await taskService.Get(id);
        
        //Assert
        result.Should().BeNull();
    }

    [Fact]
    public async void TaskService_Add_ReturnBool()
    {
        //Arrange
        var taskService = await SetupTaskService();
        var userTask = new UserTask
        {
            TaskName = "Task",
            TaskDescription = "Task"
        };
        
        //Act
        var result = await taskService.Add(userTask);
        
        //Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async void TaskService_Update_ReturnTrue()
    {
        //Arrange
        var taskService = await SetupTaskService();
        var taskDTO = new TaskDTO
        {
            userTaskID = 1,
            TaskName = "updated name",
            TaskDescription = "updated description"
        };
        
        //Act
        var result = await taskService.Update(taskDTO);
        var updatedTask = await taskService.Get(taskDTO.userTaskID);
        
        //Assert
        result.Should().BeTrue();
        updatedTask.Should().BeEquivalentTo(taskDTO);
    }
    
    [Fact]
    public async void TaskService_Update_ReturnFalse()
    {
        //Arrange
        var taskService = await SetupTaskService();
        var taskDTO = new TaskDTO
        {
            userTaskID = 11,
            TaskName = "updated name",
            TaskDescription = "updated description"
        };
        
        //Act
        var result = await taskService.Update(taskDTO);

        //Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public async void TaskService_Delete_ReturnTrue()
    {
        //Arrange
        long id = 1;
        var taskService = await SetupTaskService();
        var userTask = await taskService.Get(1);
        
        //Act
        var result = await taskService.Delete(userTask);
        var tasks = await taskService.GetAll();
        var contains = tasks.Contains(userTask);
        
        //Assert
        result.Should().BeTrue();
        contains.Should().BeFalse();
    }
    
    [Fact]
    public async void TaskService_Delete_ReturnFalse()
    {
        //Arrange
        var taskService = await SetupTaskService();
        
        //Act
        var result = await taskService.Delete(null);

        //Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async void TaskService_CreateTask_ReturnUserTask()
    {
        //Arrange
        var taskService = await SetupTaskService();
        var taskDTO = new TaskDTO
        {
            TaskName = "Task 11 name",
            TaskDescription = "Task 11 description"
        };
        var user = new User
        {
            userID = 2,
            Username = "user2name",
            Password = "pass"
        };
        
        //Act
        var result = taskService.CreateTask(taskDTO, user);
        
        //Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(taskDTO);
        result.User.Should().BeEquivalentTo(user);
    }

    [Fact]
    public async void TaskService_GetUsersTasks_ReturnUserTaskCollection()
    {
        //Arrange
        var taskService = await SetupTaskService();
        var id = 1;
        
        //Act
        var result = await taskService.GetUsersTask(id);
        
        //Assert
        result.Should().BeOfType<List<UserTask>>();
        result.Count.Should().Be(10);
    }
}

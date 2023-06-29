using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Models;
using TaskManagerApi.Service;

namespace TaskManagerApi.Tests.Services;

public class TaskServiceTests
{
    private async Task<TaskManagerContext> GetDbContext()
    {
        var options = new DbContextOptionsBuilder<TaskManagerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        
        var databaseContext = new TaskManagerContext(options);
        await databaseContext.Database.EnsureCreatedAsync();

        for (int i = 0; i < 10; i++)
        {
            databaseContext.Add(
                new UserTask
                {
                    userTaskID = i + 1,
                    TaskName = $"Task {i + 1} name",
                    TaskDescription = $"Task {i + 1} description",
                    User = new User
                    {
                        Username = "user1name",
                        Password = "pass",
                    }
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
        Assert.Equal(id, result.userTaskID);
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
    public async void TaskService_Delete_SuccessfullyDeleted()
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
    public async void TaskService_Delete_DeleteFailed()
    {
        //Arrange
        var taskService = await SetupTaskService();
        
        //Act
        var result = await taskService.Delete(null);

        //Assert
        result.Should().BeFalse();
    }
    
    private async Task<ITaskService> SetupTaskService()
    {
        var dbContext = await GetDbContext();
        ITaskService taskService = new TaskService(dbContext);

        return taskService;
    }
}
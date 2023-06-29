using FakeItEasy;
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
        var dbContext = await GetDbContext();
        var taskService = new TaskService(dbContext);
        
        //Act
        var result = await taskService.GetAll();
        
        //Assert
        result.Should().BeOfType<List<UserTask>>();
    }
}
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
        
        if (await databaseContext.UserTasks.CountAsync() < 0)
        {
            for (int i = 0; i < 10; i++)
            {
                databaseContext.Add(
                    new UserTask
                    {
                        TaskName = $"Task {i+1} name",
                        TaskDescription = $"Task {i+1} description",
                        User = new User
                        {
                            Username = "user1name",
                            Password = "pass",
                        }
                    }
                );
                await databaseContext.SaveChangesAsync();
            }
        }
        return databaseContext;
    }

}
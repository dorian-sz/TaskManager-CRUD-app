using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Models;
using TaskManagerApi.Service;

namespace TaskManagerApi.Tests.Services;

public class UserServiceTests
{
    private async Task<IUserService> SetupUserService()
    {
        var dbContext = await SetupDbContext();
        IUserService userService = new UserService(dbContext);

        return userService;
    }

    private async Task<TaskManagerContext> SetupDbContext()
    {
        var options = new DbContextOptionsBuilder<TaskManagerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        
        var databaseContext = new TaskManagerContext(options);
        await databaseContext.Database.EnsureCreatedAsync();
        
        for (int i = 1; i <= 10; i++)
        {
            databaseContext.Add(
                new User
                {
                    userID = i,
                    Username = $"user{i}name",
                    Password = "pass",
                }
            );
            await databaseContext.SaveChangesAsync();
        }
        return databaseContext;
    }
}
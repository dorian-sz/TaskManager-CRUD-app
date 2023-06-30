using FluentAssertions;
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

    [Fact]
    public async void UserService_GetAll_ReturnList()
    {
        //Arrange
        var userService = await SetupUserService();
        
        //Act
        var result = await userService.GetAll();
        
        //Assert
        result.Should().BeOfType<List<User>>();
        result.Count.Should().Be(10);
    }

    [Fact]
    public async void UserService_Get_ReturnUser()
    {
        //Arrange
        var userService = await SetupUserService();
        long id = 1;
        
        //Act
        var result = await userService.Get(id);
        
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<User>();
        result.userID.Should().Be(id);
    }
}
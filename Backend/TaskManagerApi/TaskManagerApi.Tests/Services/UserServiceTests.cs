using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Models;
using TaskManagerApi.Models.DTOs;
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

    [Fact]
    public async void UserService_Get_ReturnNull()
    {
        //Arrange
        var userService = await SetupUserService();
        long id = 11;
        
        //Act
        var result = await userService.Get(id);
        
        //Assert
        result.Should().BeNull();
    }

    [Fact]
    public async void UserService_Get_FromLoginDTOArgument_ReturnUser()
    {
        //Arrange
        var userService = await SetupUserService();
        var loginDTO = new LoginDTO
        {
            Username = "user1name",
            Password = "pass"
        };

        //Act
        var result = await userService.Get(loginDTO);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<User>();
        result.Should().BeEquivalentTo(loginDTO);
    }

    [Fact]
    public async void UserService_Add_ReturnBool()
    {
        //Arrange
        var userService = await SetupUserService();
        var user = new User
        {
            userID = 11,
            Username = "user11name",
            Password = "pass"
        };
        
        //Act
        var result = await userService.Add(user);
        var added = await userService.Get(user.userID);
        
        //Assert
        result.Should().BeTrue();
        added.Should().NotBeNull();
        added.Should().BeOfType<User>();
        added.Should().BeEquivalentTo(user);
    }

    [Fact]
    public async void UserService_Update_ReturnTrue()
    {
        //Arrange
        var userService = await SetupUserService();
        var userDTO = new UserDTO
        {
            userID = 1,
            Username = "updated username",
            Password = "updated password"
        };
        
        //Act
        var result = await userService.Update(userDTO);
        var updatedUser = await userService.Get(userDTO.userID);
        
        //Assert
        result.Should().BeTrue();
        updatedUser.Should().BeEquivalentTo(userDTO);
    }
    
    [Fact]
    public async void UserService_Update_ReturnFalse()
    {
        //Arrange
        var userService = await SetupUserService();
        var userDTO = new UserDTO
        {
            userID = 11,
            Username = "updated username",
            Password = "updated password"
        };
        
        //Act
        var result = await userService.Update(userDTO);
        
        //Assert
        result.Should().BeFalse();
    }
}
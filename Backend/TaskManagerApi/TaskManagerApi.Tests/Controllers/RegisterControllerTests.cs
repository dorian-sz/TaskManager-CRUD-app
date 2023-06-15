using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Controllers;
using TaskManagerApi.Models.DTOs;
using TaskManagerApi.Service;

namespace TaskManagerApi.Tests.Controllers;

public class RegisterControllerTests
{
    private readonly IUserService _userService;

    public RegisterControllerTests()
    {
        _userService = A.Fake<IUserService>();
    }
    
    
    [Fact]
    public async void RegisterController_Register_ReturnConflict()
    {
        //Arrange
        var expectedStatusCode = 409;
        var userDto = A.Fake<UserDTO>();
        A.CallTo(() => _userService.UserExists(userDto.Username)).Returns(true);
        var controller = new RegisterController(_userService);
        
        //Act
        var result = await controller.Register(userDto);
        var createdResult = result as StatusCodeResult;
        
        //Assert
        A.CallTo(() => _userService.UserExists(userDto.Username)).MustHaveHappenedOnceExactly();
        result.Should().NotBeNull();
        Assert.Equal(expectedStatusCode, createdResult.StatusCode);
    }
    
    [Fact]
    public async void RegisterController_Register_ReturnCreated()
    {
        //Arrange
        var expectedStatusCode = 201;
        var userDto = A.Fake<UserDTO>();
        A.CallTo(() => _userService.UserExists(userDto.Username)).Returns(false);
        var controller = new RegisterController(_userService);
        
        //Act
        var result = await controller.Register(userDto);
        var createdResult = result as StatusCodeResult;
        
        //Assert
        A.CallTo(() => _userService.UserExists(userDto.Username)).MustHaveHappenedOnceExactly();
        result.Should().NotBeNull();
        Assert.Equal(expectedStatusCode, createdResult.StatusCode);
    }
}
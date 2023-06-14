using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Controllers;
using TaskManagerApi.Models;
using TaskManagerApi.Models.DTOs;
using TaskManagerApi.Service;

namespace TaskManagerApi.Tests.Controllers;

public class UserControllerTests
{
    private readonly IUserService _service;
    private readonly IMapper _mapper;
    
    public UserControllerTests()
    {
        _service = A.Fake<IUserService>();
        _mapper = A.Fake<IMapper>();
    }

    [Fact]
    public async void UserController_GetUsers_ReturnUserDTOCollection()
    {
        //Arrange
        var users = A.Fake<ICollection<User>>();
        var userCollection = A.Fake<ICollection<UserDTO>>();
        A.CallTo(() => _mapper.Map<ICollection<UserDTO>>(users)).Returns(userCollection);
        var controller = new UserController(_service, _mapper);
        
        
        //Act
        var result = await controller.GetUsers();
    
        //Assert
        A.CallTo(() => _service.GetAll()).MustHaveHappenedOnceExactly();
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(ActionResult<ICollection<User>>));
    }

    [Fact]
    public async void UserController_GetUser_ReturnUser()
    {
        //Arrange
        var userDto = A.Fake<UserDTO>();
        var user = A.Fake<User>();
        long userID = 1;
        A.CallTo(() => _mapper.Map<UserDTO>(user)).Returns(userDto);
        var controller = new UserController(_service, _mapper);

        //Act
        var result = await controller.GetUser(userID);
        
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(ActionResult<User>));
    }

}
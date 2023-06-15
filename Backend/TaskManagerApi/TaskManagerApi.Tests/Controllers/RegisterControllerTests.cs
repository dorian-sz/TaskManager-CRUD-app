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
}
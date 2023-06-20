using System.Security.Claims;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using TaskManagerApi.Controllers;
using TaskManagerApi.Helper;
using TaskManagerApi.Models;
using TaskManagerApi.Models.DTOs;
using TaskManagerApi.Service;

namespace TaskManagerApi.Tests.Controllers;

public class AuthControllerTests
{
    private readonly IUserService _service;
    private readonly IAuthService _authService;
    private readonly IJWTSerivce _ijwtSerivce;

    public AuthControllerTests()
    {
        _service = A.Fake<IUserService>();
        _ijwtSerivce = A.Fake<IJWTSerivce>();
        _authService = A.Fake<IAuthService>();
    }
    
}
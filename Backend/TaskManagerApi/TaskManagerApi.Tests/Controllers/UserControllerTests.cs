using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Controllers;
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

}
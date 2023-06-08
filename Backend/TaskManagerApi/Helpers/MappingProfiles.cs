using AutoMapper;
using TaskManagerApi.Models;
using TaskManagerApi.Models.DTOs;

namespace TaskManagerApi.Helper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<UserTask, TaskDTO>();
        CreateMap<User, UserDTO>();
    }
}
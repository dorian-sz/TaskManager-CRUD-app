using TaskManagerApi.Models;
using TaskManagerApi.Models.DTOs;

namespace TaskManagerApi.Service;

public interface IUserService : IService<User>
{
    Task<User> CreateUserFromDTO(UserDTO userDto);
}
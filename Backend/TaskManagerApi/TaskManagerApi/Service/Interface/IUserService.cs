using TaskManagerApi.Models;
using TaskManagerApi.Models.DTOs;

namespace TaskManagerApi.Service;

public interface IUserService : IService<User>
{
    Task<bool> Update(UserDTO entity);
    Task<User?> Get(LoginDTO loginDto);
    User CreateUser(UserDTO userDto);
    Task<bool> UserExists(string username);
}
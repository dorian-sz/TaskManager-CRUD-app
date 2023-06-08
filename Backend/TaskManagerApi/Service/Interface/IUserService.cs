using TaskManagerApi.Models;
using TaskManagerApi.Models.DTOs;

namespace TaskManagerApi.Service;

public interface IUserService : IService<User>
{
    Task<bool> Update(UserDTO entity);
    User CreateUser(UserDTO userDto);
}
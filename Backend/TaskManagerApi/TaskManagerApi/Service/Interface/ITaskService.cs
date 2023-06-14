using TaskManagerApi.Models;
using TaskManagerApi.Models.DTOs;

namespace TaskManagerApi.Service;

public interface ITaskService: IService<UserTask>
{
    Task<bool> Update(TaskDTO entity);
    UserTask CreateTask(TaskDTO taskDto, User user);
    Task<ICollection<UserTask>> GetUsersTask(long id);
}
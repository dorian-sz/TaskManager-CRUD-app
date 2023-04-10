using TaskManagerApi.Models;
using TaskManagerApi.Models.DTOs;

namespace TaskManagerApi.Service;

public interface ITaskService : IService<UserTask>
{
    Task<bool> AssignUser(User user, long id);
    Task<UserTask> CreateTaskFromDto(UserTaskDTO userTaskDto);
}
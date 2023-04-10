using TaskManagerApi.Models;
using TaskManagerApi.Models.DTOs;

namespace TaskManagerApi.Service;

public interface ITaskService : IService<UserTask>
{
    Task AssignUser(User user);
    UserTask CreateTaskFromDto(UserTaskDTO userTaskDto);
}
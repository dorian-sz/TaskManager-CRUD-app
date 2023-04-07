using TaskManagerApi.Models;

namespace TaskManagerApi.Service;

public interface ITaskService : IService<Task>
{
    void AssignUser(User user);
}
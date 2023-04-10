using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Models;
using TaskManagerApi.Models.DTOs;

namespace TaskManagerApi.Service;

public class TaskService : ITaskService
{
    private readonly TaskManagerContext _context;

    public TaskService(TaskManagerContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserTask>> GetAll()
    {
        return await _context.UserTasks.ToListAsync();
    }

    public async Task<UserTask?> Get(long id)
    {
        return await _context.UserTasks.FirstOrDefaultAsync(t => t.ID == id);
    }

    public async Task Add(UserTask entity)
    {
        Console.WriteLine(entity);
        await _context.UserTasks.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> Update(long id)
    {
        var task = await Get(id);
        if (task != null)
        {
            _context.UserTasks.Entry(task).CurrentValues.SetValues(task);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task<bool> Delete(long id)
    {
        var task = await Get(id);
        if (task != null)
        {
            _context.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task<bool> AssignUser(User user, long id)
    {
        var task = await _context.UserTasks.Include(userTask => userTask.Users).FirstOrDefaultAsync(userTask => userTask.ID == id);
        if (task != null && !await UserHasTask(task, user.ID))
        {
            task.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task<UserTask> CreateTaskFromDto(UserTaskDTO userTaskDto)
    {
        return await Task.FromResult(new UserTask
        {
            TaskName = userTaskDto.TaskName,
            TaskDescription = userTaskDto.TaskDescription
        });
    }

    private async Task<bool> UserHasTask(UserTask task, long userID)
    {
        return await Task.FromResult(task.Users.Any(u => u.ID == userID));
    }
}
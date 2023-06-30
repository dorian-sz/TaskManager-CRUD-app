using AutoMapper;
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

    public async Task<ICollection<UserTask>> GetAll()
    {
        return await _context.UserTasks.ToListAsync();
    }

    public async Task<UserTask?> Get(long id)
    {
        return await _context.UserTasks.FirstOrDefaultAsync(task => task.userTaskID == id);
    }

    public async Task<bool> Add(UserTask entity)
    {
        _context.UserTasks.Add(entity);
        return await Save();
    }
    
    public async Task<bool> Update(TaskDTO entity)
    {
        var task = await Get(entity.userTaskID);
        if (task != null)
        {
            _context.UserTasks.Entry(task).CurrentValues.SetValues(entity);
        }

        return await Save();
    }

    public async Task<bool> Delete(UserTask? entity)
    {
        if (entity != null)
        {
            _context.UserTasks.Remove(entity);
        }

        return await Save();
    }

    public async Task<bool> Save()
    {
        var saved = await _context.SaveChangesAsync();
        return saved > 0;
    }

    public UserTask CreateTask(TaskDTO taskDto, User user)
    {
        return new UserTask { TaskName = taskDto.TaskName, TaskDescription = taskDto.TaskDescription, User = user};
    }
    public async Task<ICollection<UserTask>> GetUsersTask(long id)
    {
        return await _context.UserTasks.Where(task => task.User.userID == id).ToListAsync();
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using TaskManagerApi.Models;

namespace TaskManagerApi;

public class TaskManagerContext : DbContext
{
    public virtual DbSet<UserTask> UserTasks { get; set; }
    public virtual DbSet<User> Users { get; set; }

    public TaskManagerContext(DbContextOptions<TaskManagerContext> options) : base(options)
    {
        
    }
    
    public async Task<bool> Save()
    {
        var saved = await SaveChangesAsync();
        return saved > 0;
    }
}
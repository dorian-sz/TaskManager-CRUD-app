using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using TaskManagerApi.Models;
using TaskManagerApi.Models.DTOs;

namespace TaskManagerApi.Service;

public class UserService : IUserService
{
    private readonly TaskManagerContext _context;
    public UserService(TaskManagerContext context)
    {
        _context = context;
    }

    public async Task<ICollection<User>> GetAll()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> Get(long id)
    {
        return await _context.Users.FirstOrDefaultAsync(task => task.userID == id);
    }

    public async Task<bool> Add(User entity)
    {
        await _context.Users.AddAsync(entity);
        return await Save();
    }

    public async Task<bool> Update(UserDTO entity)
    {
        var task = await Get(entity.userID);
        if (task != null)
        {
            _context.Users.Entry(task).CurrentValues.SetValues(entity);
        }

        return await Save();
    }

    public async Task<bool> Delete(User? entity)
    {
        if (entity != null)
        {
            _context.Users.Remove(entity);
        }

        return await Save();
    }

    public async Task<bool> Save()
    {
        var saved = await _context.SaveChangesAsync();
        return saved > 0;
    }
    
    public User CreateUser(UserDTO userDto)
    {
        return new User { Username = userDto.Username, Password = userDto.Password };
    }

    public async Task<bool> UserExists(string username)
    {
        return await _context.Users.AnyAsync(user => user.Username == username);
    }
}
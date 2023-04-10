using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Models;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Models.DTOs;

namespace TaskManagerApi.Service;

public class UserService : IService<User>, IUpdateService<UserDTO>
{
    private readonly TaskManagerContext _context;

    public UserService(TaskManagerContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<User>> GetAll()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> Get(long id)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.ID == id);
    }

    public async Task Add(User entity)
    {
        await _context.Users.AddAsync(entity);
        await _context.SaveChangesAsync();
    }
    
    public async Task<bool> Delete(long id)
    {
        var user = await Get(id);
        if (user != null)
        {
            _context.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task<bool> Update(long id, UserDTO entity)
    {
        var user = await Get(id);
        if (user != null)
        {
            _context.Users.Entry(user).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }
}
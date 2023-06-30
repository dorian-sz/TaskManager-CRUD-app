using TaskManagerApi.Models;

namespace TaskManagerApi.Service;

public interface IService<T>
{
    Task<ICollection<T>> GetAll();
    Task<T?> Get(long id);
    Task<bool> Add(T entity);
    Task<bool> Delete(T entity);
}
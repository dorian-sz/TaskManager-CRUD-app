using TaskManagerApi.Models;

namespace TaskManagerApi.Service;

public interface IService<T>
{
    Task<IEnumerable<T>> GetAll();
    Task<T?> Get(long id);
    Task Add(T entity);
    Task<bool> Delete(long id);
}
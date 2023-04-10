namespace TaskManagerApi.Service;

public interface IUpdateService <T>
{
    Task<bool> Update(long id, T entity);
}
namespace TaskManagerApi.Service;

public interface IService<T>
{
    IEnumerable<T> GetAll();
    T Get();
    void Add(T entity);
    void Update(T entity);
    void Delete(long id);
}
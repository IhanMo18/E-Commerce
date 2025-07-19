namespace Ecommerce.Domain.Interface.Repository;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetAsync(int id);
    void Add(T obj);
    void Update(T obj);
    void Save();
    public void Remove(T obj);
}
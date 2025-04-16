using Ecommerce.Domain.Interface.Repository;
using Ecommerce.Domain.Interface.Service;

namespace Ecommerce.Services;

public class Service<T>(IRepository<T> repository) : IService<T>
    where T : class
{

    public async Task<IEnumerable<T?>> GetAllAsync()
    {
        return await repository.GetAllAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await repository.GetAsync(id);
    }

    public void Save()
    {
        repository.Save();
    }

    public void Update(T? obj)
    {
        repository.Update(obj);
    }

    public void Remove(T obj)
    {
        repository.Remove(obj);
    }
}
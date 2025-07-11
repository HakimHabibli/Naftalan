namespace NaftalanHotelSystem.Application.Abstractions.Repositories;

public interface IWriteRepository<T> : IRepository<T> where T : class 
{
    Task CreateAsync(T entity);
    void Remove(T entity);
    void Update(T entity);

    void RemoveRange(IEnumerable<T> entities);
}
namespace NaftalanHotelSystem.Application.Abstractions.Repositories;

public interface IReadRepository<T> : IRepository<T> where T : class
{
    IQueryable<T> GetAll(bool asNoTracking = true);
    Task<T?> GetByIdAsync(int id);
}

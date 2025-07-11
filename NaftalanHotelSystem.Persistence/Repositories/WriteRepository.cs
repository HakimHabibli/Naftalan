using Microsoft.EntityFrameworkCore;
using NaftalanHotelSystem.Application.Abstractions.Repositories;
using NaftalanHotelSystem.Persistence.DataAccessLayer;

namespace NaftalanHotelSystem.Persistence.Repositories;

public class WriteRepository<T> : IWriteRepository<T> where T : class
{
    protected readonly AppDbContext _appDbContext;
    public WriteRepository(AppDbContext appDbContext)
    {
        _appDbContext= appDbContext;
    }
    public DbSet<T> Table => _appDbContext.Set<T>();

    public async Task CreateAsync(T entity)
    {
        await Table.AddAsync(entity);
        await _appDbContext.SaveChangesAsync();
    }

    public void Remove(T entity)
    {
        Table.Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        Table.RemoveRange(entities);
    }
    public void Update(T entity)
    {
        Table.Update(entity);
    }


}
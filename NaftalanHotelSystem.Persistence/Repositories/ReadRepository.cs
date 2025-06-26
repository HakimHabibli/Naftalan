using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NaftalanHotelSystem.Application.Abstractions.Repositories;
using NaftalanHotelSystem.Domain.Common;
using NaftalanHotelSystem.Persistence.DataAccessLayer;

namespace NaftalanHotelSystem.Persistence.Repositories;

public class ReadRepository<T> : IReadRepository<T> where T : class
{
    protected readonly AppDbContext _appDbContext;
    public DbSet<T> Table => _appDbContext.Set<T>();
    public ReadRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    

    public async Task<T?> GetByIdAsync(int id)
    {
        return await Table.FirstOrDefaultAsync(x => (x as BaseEntity).Id == id);
    }

    public IQueryable<T> GetAll(bool asNoTracking = true)
    {
        return asNoTracking ? Table.AsNoTracking() : Table;
    }
}

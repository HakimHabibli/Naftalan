using Microsoft.EntityFrameworkCore;

namespace NaftalanHotelSystem.Application.Abstractions.Repositories;
public interface IRepository<T> where T : class
{
     DbSet<T> Table { get; }
   
}

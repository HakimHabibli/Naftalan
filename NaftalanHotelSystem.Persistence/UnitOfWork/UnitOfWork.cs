using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NaftalanHotelSystem.Application.Abstractions.Repositories;
using NaftalanHotelSystem.Application.Abstractions.UnitOfWork;
using NaftalanHotelSystem.Domain.Entites;
using NaftalanHotelSystem.Persistence.DataAccessLayer;
using NaftalanHotelSystem.Persistence.Repositories;

namespace NaftalanHotelSystem.Persistence.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IWriteRepository<Room> RoomWriteRepository => new WriteRepository<Room>(_context);
    public IReadRepository<Room> RoomReadRepository => new ReadRepository<Room>(_context);

    public IWriteRepository<Equipment> EquipmentWriteRepository => new WriteRepository<Equipment>(_context);
    public IReadRepository<Equipment> EquipmentReadRepository => new ReadRepository<Equipment>(_context);

    public IReadRepository<TreatmentMethod> TreatmentMethodReadRepository => new ReadRepository<TreatmentMethod>(_context);

    public IWriteRepository<TreatmentMethod> TreatmentMethodWriteRepository => new WriteRepository<TreatmentMethod>(_context);

   
    public async Task<int> SaveChangesAsync()
    {
        return  await _context.SaveChangesAsync();
    }
}

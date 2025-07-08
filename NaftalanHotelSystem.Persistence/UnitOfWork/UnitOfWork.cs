using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Wasm;
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

    public IReadRepository<Package> PackageReadRepository => new ReadRepository<Package>(   _context);
    public IWriteRepository<Package> PackageWriteRepository => new WriteRepository<Package>(_context);

    public IReadRepository<Illness> IllnessReadRepository =>  new ReadRepository<Illness>(_context);

    public IWriteRepository<Illness> IllnessWriteRepository => new WriteRepository<Illness>(_context);

    public IReadRepository<TreatmentCategory> TreatmentCategoryReadRepository => new ReadRepository<TreatmentCategory>(_context);

    public IWriteRepository<TreatmentCategory> TreatmentCategoryWriteRepository => new WriteRepository<TreatmentCategory>(_context);

    public IReadRepository<About> AboutReadRepository =>  new ReadRepository<About>(_context);

    public IWriteRepository<About> AboutWriteRepository =>  new WriteRepository<About>(_context);

    public IReadRepository<Contact> ContactReadRepository =>  new ReadRepository<Contact>(_context);

    public IWriteRepository<Contact> ContactWriteRepository =>  new WriteRepository<Contact>(_context);


    /*
    public IReadRepository< >   .ReadRepository => new ReadRepository<  >(_context);
    public IWriteRepository< >   .WriteRepository => new WriteRepository<  >(_context);
     */

    public async Task<int> SaveChangesAsync()
    {
        return  await _context.SaveChangesAsync();
    }
}

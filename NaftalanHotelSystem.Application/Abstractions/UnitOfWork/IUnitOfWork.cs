using NaftalanHotelSystem.Application.Abstractions.Repositories;
using NaftalanHotelSystem.Domain.Entites;

namespace NaftalanHotelSystem.Application.Abstractions.UnitOfWork;

public interface IUnitOfWork
{
    IWriteRepository<Room> RoomWriteRepository { get; } 
    IReadRepository<Room> RoomReadRepository { get;  }

    IWriteRepository<Equipment> EquipmentWriteRepository { get; }
    IReadRepository<Equipment> EquipmentReadRepository { get;  }
   
    IReadRepository<TreatmentMethod>TreatmentMethodReadRepository {get;}
    
    IWriteRepository<TreatmentMethod> TreatmentMethodWriteRepository { get;}
    /*
     IReadRepository< >  .ReadRepository {get;}
     IWriteRepository< >  .WriteRepository {get;}
     */

    Task<int> SaveChangesAsync();
}

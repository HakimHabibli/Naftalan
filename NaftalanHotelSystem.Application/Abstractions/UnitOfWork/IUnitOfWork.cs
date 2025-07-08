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

    IReadRepository<Package> PackageReadRepository { get;}
    IWriteRepository<Package> PackageWriteRepository { get;}

    IReadRepository<Illness> IllnessReadRepository {get;}
    IWriteRepository<Illness> IllnessWriteRepository {get;}

    IReadRepository<TreatmentCategory> TreatmentCategoryReadRepository { get;}
    IWriteRepository<TreatmentCategory> TreatmentCategoryWriteRepository { get;}

    IReadRepository<About> AboutReadRepository { get; }
    IWriteRepository<About> AboutWriteRepository { get; }

    IReadRepository<Contact> ContactReadRepository { get; }
    IWriteRepository<Contact> ContactWriteRepository { get; }
    /*
     IReadRepository< >  .ReadRepository {get;}
     IWriteRepository< >  .WriteRepository {get;}
     */

    Task<int> SaveChangesAsync();
}

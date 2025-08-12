using NaftalanHotelSystem.Application.Abstractions.Repositories;
using NaftalanHotelSystem.Domain.Entites;
using Org.BouncyCastle.Asn1.Esf;

namespace NaftalanHotelSystem.Application.Abstractions.UnitOfWork;

public interface IUnitOfWork
{
    IWriteRepository<Room> RoomWriteRepository { get; } 
    IReadRepository<Room> RoomReadRepository { get;  }

    IWriteRepository<Equipment> EquipmentWriteRepository { get; }
    IReadRepository<Equipment> EquipmentReadRepository { get;  }

    IReadRepository<RoomChild> RoomChildReadRepository { get; }
    IWriteRepository<RoomChild> RoomChildWriteRepository { get; }
    IReadRepository<TreatmentMethod>TreatmentMethodReadRepository {get;}
    IWriteRepository<TreatmentMethod> TreatmentMethodWriteRepository { get;}
    IReadRepository<TreatmentMethodTranslation> TreatmentMethodTranslationReadRepository { get; }
    IWriteRepository<TreatmentMethodTranslation> TreatmentMethodTranslationWriteRepository { get; }

    IReadRepository<RoomPriceByOccupancy> RoomPriceByOccupancyReadRepository { get; }
    IWriteRepository<RoomPriceByOccupancy> RoomPriceByOccupancyWriteRepository { get; }

    IReadRepository<Child> ChildReadRepository { get; }
    IWriteRepository<Child> ChildWriteRepository { get; }

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

    IReadRepository<Image> ImageReadRepository { get; }
    IWriteRepository<Image> ImageWriteRepository { get; }


    IReadRepository<RoomTranslation> RoomTranslationReadRepository { get; }
    IWriteRepository<RoomTranslation> RoomTranslationWriteRepository { get; }

    IReadRepository<RoomEquipment> RoomEquipmentReadRepository { get; }
    IWriteRepository<RoomEquipment> RoomEquipmentWriteRepository { get; }


    IReadRepository<IllnessTranslation> IllnessTranslationReadRepository { get; }
    IWriteRepository<IllnessTranslation> IllnessTranslationWriteRepository { get; }
    /*
     IReadRepository< >  .ReadRepository {get;}
     IWriteRepository< >  .WriteRepository {get;}
     */

    Task<int> SaveChangesAsync();
}

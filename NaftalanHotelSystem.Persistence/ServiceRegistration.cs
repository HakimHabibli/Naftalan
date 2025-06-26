using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NaftalanHotelSystem.Application.Abstractions.Repositories;
using NaftalanHotelSystem.Application.Abstractions.UnitOfWork;
using NaftalanHotelSystem.Domain.Entites;
using NaftalanHotelSystem.Persistence.Repositories;
using NaftalanHotelSystem.Persistence.UnitOfWork;

namespace NaftalanHotelSystem.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
       
          services.AddScoped(typeof(IReadRepository<>),typeof(ReadRepository<>));
          services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));

        services.AddScoped<IWriteRepository<Room>, WriteRepository<Room>>();
        services.AddScoped<IReadRepository<Room>, ReadRepository<Room>>();

        services.AddScoped<IUnitOfWork, NaftalanHotelSystem.Persistence.UnitOfWork.UnitOfWork>();
    }
    /*

    *Add-Migration InitialCreate -Project NaftalanHotelSystem.Persistence -StartupProject NaftalanHotelSystem.API

    Update-Database -Project NaftalanHotelSystem.Persistence -StartupProject NaftalanHotelSystem.API

    */
}

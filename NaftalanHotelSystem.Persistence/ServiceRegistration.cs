using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NaftalanHotelSystem.Persistence.DataAccessLayer;

namespace NaftalanHotelSystem.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
       

        
    }
    /*

    *Add-Migration InitialCreate -Project NaftalanHotelSystem.Persistence -StartupProject NaftalanHotelSystem.API

    Update-Database -Project NaftalanHotelSystem.Persistence -StartupProject NaftalanHotelSystem.API

    */
}

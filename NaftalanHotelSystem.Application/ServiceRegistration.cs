using System.Runtime.Intrinsics.Wasm;
using Microsoft.Extensions.DependencyInjection;
using NaftalanHotelSystem.Application.Abstractions.Services;
using NaftalanHotelSystem.Application.Abstractions.UnitOfWork;
using NaftalanHotelSystem.Application.Concretes.Services;
using NaftalanHotelSystem.Infrastructure.Services;

namespace NaftalanHotelSystem.Application;

public static class ServiceRegistration
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IRoomService, RoomService>();
        services.AddScoped<IEquipmentService, EquipmentService>();
        services.AddScoped<ITreatmentMethodService, TreatmentMethodService>();
        services.AddScoped<IPackageService, PackageService>();

    }
}

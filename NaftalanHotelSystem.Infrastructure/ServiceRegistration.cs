using Microsoft.Extensions.DependencyInjection;
using NaftalanHotelSystem.Application.Abstractions.Repositories;
using NaftalanHotelSystem.Application.Abstractions.Services;
using NaftalanHotelSystem.Domain.Entites;
using NaftalanHotelSystem.Infrastructure;
using NaftalanHotelSystem.Infrastructure.Services;

namespace NaftalanHotelSystem.Persistence;

public static class ServiceRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        //services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));


        services.AddScoped<INotificationService, NotificationService>();
    }

}

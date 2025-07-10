using Microsoft.Extensions.DependencyInjection;
using NaftalanHotelSystem.Application.Abstractions.Services;
using NaftalanHotelSystem.Application.Concretes.Services;
using NaftalanHotelSystem.Infrastructure;

namespace NaftalanHotelSystem.Persistence;

public static class ServiceRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        //services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));


        services.AddScoped<INotificationService, NotificationService>();


        services.AddScoped<IImageService, ImageService>();
    }

}

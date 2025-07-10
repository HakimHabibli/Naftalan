using NaftalanHotelSystem.Application.Concretes.Services;
using NaftalanHotelSystem.Domain.Entites;

namespace NaftalanHotelSystem.Application.Abstractions.Services;

public interface IAboutService 
{
    public Task<AboutDto> GetAboutAsync();
    public Task UpdateAboutAsync(AboutUpdateDto about);
}

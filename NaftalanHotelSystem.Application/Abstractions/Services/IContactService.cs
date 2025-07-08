using NaftalanHotelSystem.Application.Concretes.Services;
using NaftalanHotelSystem.Domain.Entites;

namespace NaftalanHotelSystem.Application.Abstractions.Services;

public interface IContactService
{
    public Task<ContactDto> GetContactAsync();
    public Task UpdateContactAsync(ContactUpdateDto contact);
}


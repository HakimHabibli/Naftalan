using Microsoft.EntityFrameworkCore;
using NaftalanHotelSystem.Application.Abstractions.Services;
using NaftalanHotelSystem.Application.Abstractions.UnitOfWork;
using NaftalanHotelSystem.Application.DataTransferObject;

namespace NaftalanHotelSystem.Application.Concretes.Services;

public class ContactService : IContactService
{
    private readonly IUnitOfWork _unitOfWork;

    public ContactService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ContactDto> GetContactAsync()
    {
        var contact = await _unitOfWork.ContactReadRepository.Table.FirstOrDefaultAsync();

        if (contact is null) return null;

        return new ContactDto
        {
            Id = contact.Id,
            Number = contact.Number,
            Mail = contact.Mail,
            Adress = contact.Adress,
            InstagramLink = contact.InstagramLink,
            FacebookLink = contact.FacebookLink,
            TiktokLink = contact.TiktokLink,
            YoutubeLink = contact.YoutubeLink,
            WhatsappNumber = contact.WhatsappNumber
        };
    }

    public async Task UpdateContactAsync(ContactUpdateDto dto)
    {
        var existing = await _unitOfWork.ContactWriteRepository.Table.FirstOrDefaultAsync();

        if (existing is not null)
        {
            existing.Number = dto.Number;
            existing.Mail = dto.Mail;
            existing.Adress = dto.Adress;
            existing.InstagramLink = dto.InstagramLink;
            existing.FacebookLink = dto.FacebookLink;
            existing.TiktokLink = dto.TiktokLink;
            existing.YoutubeLink = dto.YoutubeLink;
            existing.WhatsappNumber = dto.WhatsappNumber;

            _unitOfWork.ContactWriteRepository.Update(existing);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}


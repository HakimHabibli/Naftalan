using System;
using Microsoft.EntityFrameworkCore;
using NaftalanHotelSystem.Application.Abstractions.Services;
using NaftalanHotelSystem.Application.Abstractions.UnitOfWork;
using NaftalanHotelSystem.Domain.Entites;
using NaftalanHotelSystem.Domain.Enums;

namespace NaftalanHotelSystem.Application.Concretes.Services;

public class AboutService : IAboutService
{
    private readonly IUnitOfWork _unitOfWork;

    public AboutService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<AboutDto> GetAboutAsync()
    {
        var about = await _unitOfWork.AboutReadRepository
            .Table
            .Include(x => x.AboutTranslations)
            .FirstOrDefaultAsync();


        return new AboutDto
        {
            Id = about.Id,
            VideoLink = about.VideoLink,
            Translations = about.AboutTranslations.Select(t => new AboutTranslationDto
            {
                Title = t.Title,
                MiniTitle = t.MiniTitle,
                Description = t.Description,
                Language = t.Language
            }).ToList()
        };
    }

    public async Task UpdateAboutAsync(AboutUpdateDto dto)
    {
        var about = await _unitOfWork.AboutWriteRepository
            .Table
            .Include(x => x.AboutTranslations)
            .FirstOrDefaultAsync(x => x.Id == dto.Id);

    

        about.VideoLink = dto.VideoLink;
       

        foreach (var translationDto in dto.Translations)
        {
            var existingTranslation = about.AboutTranslations
                .FirstOrDefault(t => t.Language == translationDto.Language);

            if (existingTranslation != null)
            {
                
                existingTranslation.Title = translationDto.Title;
                existingTranslation.MiniTitle = translationDto.MiniTitle;
                existingTranslation.Description = translationDto.Description;
            }
            else
            {
             
                about.AboutTranslations.Add(new AboutTranslation
                {
                    Title = translationDto.Title,
                    MiniTitle = translationDto.MiniTitle,
                    Description = translationDto.Description,
                    Language = translationDto.Language
                });
            }
        }

        _unitOfWork.AboutWriteRepository.Update(about);
        await _unitOfWork.SaveChangesAsync();
    }

}











public class AboutDto
{
    public int Id { get; set; }
    public string VideoLink { get; set; }

    public List<AboutTranslationDto> Translations { get; set; }
}

public class AboutTranslationDto
{
    public string Title { get; set; }
    public string MiniTitle { get; set; }
    public string Description { get; set; }
    public Language Language { get; set; }
}
public class AboutUpdateDto
{
    public int Id { get; set; }
    public string VideoLink { get; set; }

    public List<AboutTranslationUpdateDto> Translations { get; set; }
}

public class AboutTranslationUpdateDto
{
    public int? Id { get; set; } 
    public string Title { get; set; }
    public string MiniTitle { get; set; }
    public string Description { get; set; }
    public Language Language { get; set; }
}
public class ContactDto
{
    public int Id { get; set; }
    public List<string> Number { get; set; }
    public string Mail { get; set; }
    public string Adress { get; set; }
    public string InstagramLink { get; set; }
    public string FacebookLink { get; set; }
    public string TiktokLink { get; set; }
    public string YoutubeLink { get; set; }
    public string WhatsappNumber { get; set; }
}

public class ContactUpdateDto
{
    public List<string> Number { get; set; }
    public string Mail { get; set; }
    public string Adress { get; set; }
    public string InstagramLink { get; set; }
    public string FacebookLink { get; set; }
    public string TiktokLink { get; set; }
    public string YoutubeLink { get; set; }
    public string WhatsappNumber { get; set; }
}

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


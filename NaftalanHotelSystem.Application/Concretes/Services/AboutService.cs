using System;
using Microsoft.EntityFrameworkCore;
using NaftalanHotelSystem.Application.Abstractions.Services;
using NaftalanHotelSystem.Application.Abstractions.UnitOfWork;
using NaftalanHotelSystem.Application.DataTransferObject;
using NaftalanHotelSystem.Domain.Entites;

namespace NaftalanHotelSystem.Application.Concretes.Services;

public class AboutService : IAboutService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IImageService _imageService;

    public AboutService(IUnitOfWork unitOfWork, IImageService imageService)
    {
        _unitOfWork = unitOfWork;
        _imageService = imageService;
    }

    public async Task<AboutDto> GetAboutAsync()
    {
        var about = await _unitOfWork.AboutReadRepository
            .Table
            .Include(x => x.AboutTranslations)
            .FirstOrDefaultAsync();

        var image = await _unitOfWork.ImageReadRepository.GetAll()
            .Where(x => x.Entity == ImageEntity.About && x.RelatedEntityId == about.Id)
        .OrderByDescending(x => x.Id)
        .FirstOrDefaultAsync();

        return new AboutDto
        {
            VideoLink = about.VideoLink,
            ImageUrl = image?.Url,
            Translations = about.AboutTranslations.Select(t => new AboutTranslationDto
            {
                Id = t.Id,
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
            .Include(a => a.AboutTranslations)
            .FirstOrDefaultAsync(a => a.Id == dto.Id);

        if (about == null)
            throw new Exception("About not found");

        
        about.VideoLink = dto.VideoLink;

      
        if (dto.ImageFile != null && dto.ImageFile.Length > 0)
        {
            var imageDto = new ImageCreateDto
            {
                File = dto.ImageFile,
                Entity = ImageEntity.About,
                RelatedEntityId = about.Id
            };

            var imageUrl = await _imageService.UploadImageAsync(imageDto);

            await _unitOfWork.ImageWriteRepository.CreateAsync(new Domain.Entites.Image
            {
                Url = imageUrl.Url,
                Entity = ImageEntity.About,
                RelatedEntityId = about.Id
            });
        }

        
        foreach (var s in dto.Translations)
        {
            var existing = about.AboutTranslations
                .FirstOrDefault(t => t.Id == s.Id && t.Language == s.Language);

            if (existing != null)
            {
                existing.Title = s.Title;
                existing.MiniTitle = s.MiniTitle;
                existing.Description = s.Description;
            }
            else
            {
                about.AboutTranslations.Add(new AboutTranslation
                {
                    Title = s.Title,
                    MiniTitle = s.MiniTitle,
                    Description = s.Description,
                    Language = s.Language,
                    AboutId = about.Id
                });
            }
        }

        await _unitOfWork.SaveChangesAsync();
    }

}


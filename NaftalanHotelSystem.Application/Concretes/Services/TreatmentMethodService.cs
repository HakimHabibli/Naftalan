using Microsoft.EntityFrameworkCore;
using NaftalanHotelSystem.Application.Abstractions.Services;
using NaftalanHotelSystem.Application.Abstractions.UnitOfWork;
using NaftalanHotelSystem.Application.DataTransferObject.Image;
using NaftalanHotelSystem.Application.DataTransferObject.TreatmentMethod;
using NaftalanHotelSystem.Domain.Entites;
using NaftalanHotelSystem.Domain.Enums;

namespace NaftalanHotelSystem.Application.Concretes.Services;

public class TreatmentMethodService : ITreatmentMethodService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IImageService _imageService; 

    public TreatmentMethodService(IUnitOfWork unitOfWork, IImageService imageService) 
    {
        _unitOfWork = unitOfWork;
        _imageService = imageService;
    }

    public async Task CreateTreatmentMethodAsync(TreatmentMethodCreateDto dto)
    {
        var treatment = new TreatmentMethod
        {
            Translations = dto.Translations.Select(t => new TreatmentMethodTranslation 
            {
                Description = t.Description,
                Language = t.Language,
                Name = t.Name
            }).ToList()
        };

        await _unitOfWork.TreatmentMethodWriteRepository.CreateAsync(treatment);
        await _unitOfWork.SaveChangesAsync();

        
        if (dto.ImageFile != null)
        {
            var imageCreateDto = new ImageCreateDto
            {
                File = dto.ImageFile,
                Entity = ImageEntity.TreatmentMethod, 
                RelatedEntityId = treatment.Id 
            };
            await _imageService.UploadImageAsync(imageCreateDto);
        }
    }

   
    public async Task DeleteTreatmentMethodAsync(int id)
    {
        var treatment = await _unitOfWork.TreatmentMethodWriteRepository.Table
            .Include(t => t.Translations)
            .FirstOrDefaultAsync(t => t.Id == id); 

        if (treatment == null)
        {
            throw new Exception($"Treatment method with Id {id} not found for deletion."); 
        }

        
        var imagesToDelete = await _unitOfWork.ImageReadRepository.GetAll(asNoTracking: true)
            .Where(x => x.Entity == ImageEntity.TreatmentMethod && x.RelatedEntityId == treatment.Id)
            .ToListAsync();

        foreach (var image in imagesToDelete)
        {
            await _imageService.DeleteImageAsync(image.Id);
        }

        
        if (treatment.Translations != null && treatment.Translations.Any())
        {
            _unitOfWork.TreatmentMethodTranslationWriteRepository.RemoveRange(treatment.Translations.ToList());
        }

        _unitOfWork.TreatmentMethodWriteRepository.Remove(treatment);
        await _unitOfWork.SaveChangesAsync();
    }

   
    public async Task<List<TreatmentMethodGetByIdDto>> GetAllTreatmentMethodsAsync() 
    {
        var treatments = await _unitOfWork.TreatmentMethodReadRepository.Table
            .Include(t => t.Translations).ToListAsync();

        var resultDtos = new List<TreatmentMethodGetByIdDto>(); 

        foreach (var treatment in treatments)
        {
            
            var imageUrl = await _unitOfWork.ImageReadRepository.GetAll(asNoTracking: true)
                .Where(x => x.Entity == ImageEntity.TreatmentMethod && x.RelatedEntityId == treatment.Id)
                .Select(x => x.Url)
                .FirstOrDefaultAsync();

            resultDtos.Add(new TreatmentMethodGetByIdDto
            {
                Id = treatment.Id,
                Translations = treatment.Translations.Select(tr => new TreatmentMethodTranslationGetDto 
                {
                    Id = tr.Id, 
                    Name = tr.Name,
                    Description = tr.Description,
                    Language = tr.Language
                }).ToList(),
                ImageUrl = imageUrl 
            });
        }

        return resultDtos;
    }

    
    public async Task<TreatmentMethodGetByIdDto> GetTreatmentMethodByIdAsync(int id)
    {
        var treatment = await _unitOfWork.TreatmentMethodReadRepository.Table
            .Include(t => t.Translations)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (treatment == null)
        {
            return null;
        }

        
        var imageUrl = await _unitOfWork.ImageReadRepository.GetAll(asNoTracking: true)
            .Where(x => x.Entity == ImageEntity.TreatmentMethod && x.RelatedEntityId == treatment.Id)
            .Select(x => x.Url)
            .FirstOrDefaultAsync();

        var translations = treatment.Translations.Select(t => new TreatmentMethodTranslationGetDto 
        {
            Id = t.Id, 
            Name = t.Name,
            Description = t.Description,
            Language = t.Language
        }).ToList();

        return new TreatmentMethodGetByIdDto
        {
            Id = treatment.Id,
            Translations = translations,
            ImageUrl = imageUrl 
        };
    }

   
    public async Task UpdateTreatmentMethodAsync(int id, TreatmentMethodUpdateDto dto) 
    {
        var treatment = await _unitOfWork.TreatmentMethodWriteRepository.Table
            .Include(t => t.Translations)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (treatment == null)
            throw new Exception($"Treatment method with Id {id} not found."); 

      
        var currentTranslations = treatment.Translations.ToList();

      
        var translationsToDelete = currentTranslations
            .Where(ct => !dto.Translations.Any(dt => dt.Id == ct.Id)) 
            .ToList();
        _unitOfWork.TreatmentMethodTranslationWriteRepository.RemoveRange(translationsToDelete);

        foreach (var newOrUpdatedTranslationDto in dto.Translations) 
        {
          
            var existingTranslation = currentTranslations.FirstOrDefault(ct => ct.Id == newOrUpdatedTranslationDto.Id);

            if (existingTranslation != null)
            {
                
                existingTranslation.Name = newOrUpdatedTranslationDto.Name;
                existingTranslation.Description = newOrUpdatedTranslationDto.Description;
                existingTranslation.Language = newOrUpdatedTranslationDto.Language;
                _unitOfWork.TreatmentMethodTranslationWriteRepository.Update(existingTranslation);
            }
            else
            {
                
                var newTranslation = new TreatmentMethodTranslation
                {
                    TreatmentMethodId = treatment.Id, 
                    Name = newOrUpdatedTranslationDto.Name,
                    Description = newOrUpdatedTranslationDto.Description,
                    Language = newOrUpdatedTranslationDto.Language
                };
                await _unitOfWork.TreatmentMethodTranslationWriteRepository.CreateAsync(newTranslation);
            }
        }

      
        if (dto.ImageFile != null)
        {
            var existingImage = await _unitOfWork.ImageReadRepository.GetAll(asNoTracking: true)
                .Where(x => x.Entity == ImageEntity.TreatmentMethod && x.RelatedEntityId == treatment.Id)
                .FirstOrDefaultAsync();

            if (existingImage != null)
            {
                await _imageService.DeleteImageAsync(existingImage.Id);
            }

            var imageCreateDto = new ImageCreateDto
            {
                File = dto.ImageFile,
                Entity = ImageEntity.TreatmentMethod,
                RelatedEntityId = treatment.Id
            };
            await _imageService.UploadImageAsync(imageCreateDto);
        }

        _unitOfWork.TreatmentMethodWriteRepository.Update(treatment);
        await _unitOfWork.SaveChangesAsync();
    }
}
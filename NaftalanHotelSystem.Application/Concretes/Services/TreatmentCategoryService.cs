using Microsoft.EntityFrameworkCore;
using NaftalanHotelSystem.Application.Abstractions.Services;
using NaftalanHotelSystem.Application.Abstractions.UnitOfWork;
using NaftalanHotelSystem.Application.DataTransferObject;
using NaftalanHotelSystem.Domain.Entites;
using NaftalanHotelSystem.Domain.Enums;

namespace NaftalanHotelSystem.Application.Concretes.Services;
public class TreatmentCategoryService:ITreatmentCategoryService
{
    private readonly IUnitOfWork _unitOfWork;

    public TreatmentCategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

   
    public async Task<int> CreateTreatmentCategoryAsync(TreatmentCategoryCreateDto dto)
    {
        var category = new TreatmentCategory
        {
            Translations = dto.Translations.Select(t => new TreatmentCategoryTranslation
            {
                Name = t.Name,
                Language = t.Language
            }).ToList()
        };

        await _unitOfWork.TreatmentCategoryWriteRepository.CreateAsync(category);
        await _unitOfWork.SaveChangesAsync();
        return category.Id;
    }

 
    public async Task UpdateTreatmentCategoryAsync(int id, TreatmentCategoryUpdateDto dto)
    {
        var category = await _unitOfWork.TreatmentCategoryWriteRepository.Table
            .Include(c => c.Translations)
            .FirstOrDefaultAsync(c => c.Id == id);

        //if (category == null)
        //    throw new NotFoundException($"TreatmentCategory with Id {id} not found");

       
        category.Translations = dto.Translations.Select(t => new TreatmentCategoryTranslation
        {
            Name = t.Name,
            Language = t.Language
        }).ToList();

        _unitOfWork.TreatmentCategoryWriteRepository.Update(category);
        await _unitOfWork.SaveChangesAsync();
    }

    
    public async Task DeleteTreatmentCategoryAsync(int id)
    {
        var category = await _unitOfWork.TreatmentCategoryWriteRepository.Table
            .FirstOrDefaultAsync(c => c.Id == id);

        //if (category == null)
        //    throw new NotFoundException($"TreatmentCategory with Id {id} not found");

        _unitOfWork.TreatmentCategoryWriteRepository.Remove(category);
        await _unitOfWork.SaveChangesAsync();
    }

    
    public async Task<List<TreatmentCategoryDto>> GetAllTreatmentCategoriesAsync()
    {
        var categories = await _unitOfWork.TreatmentCategoryReadRepository.Table
            .Include(c => c.Translations)
            .ToListAsync();

        return categories.Select(c => new TreatmentCategoryDto
        {
            Id = c.Id,
            Translations = c.Translations.Select(t => new TreatmentCategoryTranslationDto
            {
                Name = t.Name,
                Language = t.Language
            }).ToList()
        }).ToList();
    }

    
    public async Task<TreatmentCategoryDto> GetTreatmentCategoryByIdAsync(int id, Language? language = Language.Az)
    {
        var category = await _unitOfWork.TreatmentCategoryReadRepository.Table
            .Include(c => c.Translations)
            .FirstOrDefaultAsync(c => c.Id == id);

        //if (category == null)
        //    throw new NotFoundException($"TreatmentCategory with Id {id} not found");

        List<TreatmentCategoryTranslationDto> translations;

        if (language.HasValue)
        {
            translations = category.Translations
                .Where(t => t.Language == language)
                .Select(t => new TreatmentCategoryTranslationDto
                {
                    Name = t.Name,
                    Language = t.Language
                }).ToList();
        }
        else
        {
            translations = category.Translations.Select(t => new TreatmentCategoryTranslationDto
            {
                Name = t.Name,
                Language = t.Language
            }).ToList();
        }

        return new TreatmentCategoryDto
        {
            Id = category.Id,
            Translations = translations
        };
    }
}

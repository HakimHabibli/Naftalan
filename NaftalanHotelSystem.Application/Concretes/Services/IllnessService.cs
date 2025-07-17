using Microsoft.EntityFrameworkCore;
using NaftalanHotelSystem.Application.Abstractions.Services;
using NaftalanHotelSystem.Application.Abstractions.UnitOfWork;
using NaftalanHotelSystem.Application.DataTransferObject.Illness;
using NaftalanHotelSystem.Domain.Entites;
using NaftalanHotelSystem.Domain.Enums;

namespace NaftalanHotelSystem.Application.Concretes.Services;

public class IllnessService:IIllnessService
{
    private readonly IUnitOfWork _unitOfWork;

    public IllnessService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> CreateIllnessAsync(IllnessCreateDto dto)
    {
        var illness = new Illness
        {
            TreatmentCategoryId = dto.TreatmentCategoryId,
            Translations = dto.Translations.Select(t => new IllnessTranslation
            {
                Name = t.Name,
                Description = t.Description,
                Language = t.Language
            }).ToList()
        };

        await _unitOfWork.IllnessWriteRepository.CreateAsync(illness);
        await _unitOfWork.SaveChangesAsync();
        return illness.Id;
    }

    public async Task UpdateIllnessAsync(int id, IllnessUpdateDto dto)
    {
        var illness = await _unitOfWork.IllnessWriteRepository.Table
            .Include(i => i.Translations)
            .FirstOrDefaultAsync(i => i.Id == id);

        //if (illness == null)
        //    throw new NotFoundException($"Illness with Id {id} not found");

        illness.TreatmentCategoryId = dto.TreatmentCategoryId;

       
        illness.Translations = dto.Translations.Select(t => new IllnessTranslation
        {
            Name = t.Name,
            Description = t.Description,
            Language = t.Language
        }).ToList();

        _unitOfWork.IllnessWriteRepository.Update(illness);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteIllnessAsync(int id)
    {
        var illness = await _unitOfWork.IllnessWriteRepository.Table
            .FirstOrDefaultAsync(i => i.Id == id);

        //if (illness == null)
        //    throw new NotFoundException($"Illness with Id {id} not found");

        _unitOfWork.IllnessWriteRepository.Remove(illness);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<List<IllnessDto>> GetAllIllnessesAsync()
    {
        var illnesses = await _unitOfWork.IllnessReadRepository.Table
            .Include(i => i.Translations)
            .Include(i => i.TreatmentCategory).ThenInclude(tc => tc.Translations)
            .ToListAsync();

        return illnesses.Select(i => new IllnessDto
        {
            Id = i.Id,
            TreatmentCategoryId = i.TreatmentCategoryId,
            Translations = i.Translations.Select(t => new IllnessTranslationDto
            {
                Name = t.Name,
                Description = t.Description,
                Language = t.Language
            }).ToList()
        }).ToList();
    }

    public async Task<IllnessDto> GetIllnessByIdAsync(int id, Language? language = Language.Az)
    {
        var illness = await _unitOfWork.IllnessReadRepository.Table
            .Include(i => i.Translations)
            .Include(i => i.TreatmentCategory).ThenInclude(tc => tc.Translations)
            .FirstOrDefaultAsync(i => i.Id == id);

        //if (illness == null)
        //    throw new NotFoundException($"Illness with Id {id} not found");

        List<IllnessTranslationDto> translations;
        if (language.HasValue)
        {
            translations = illness.Translations
                .Where(t => t.Language == language)
                .Select(t => new IllnessTranslationDto
                {
                    Name = t.Name,
                    Description = t.Description,
                    Language = t.Language
                }).ToList();
        }
        else
        {
            translations = illness.Translations.Select(t => new IllnessTranslationDto
            {
                Name = t.Name,
                Description = t.Description,
                Language = t.Language
            }).ToList();
        }

        return new IllnessDto
        {
            Id = illness.Id,
            TreatmentCategoryId = illness.TreatmentCategoryId,
          Translations = translations
        };
    }
}

using Microsoft.EntityFrameworkCore;
using NaftalanHotelSystem.Application.Abstractions.Services;
using NaftalanHotelSystem.Application.Abstractions.UnitOfWork;
using NaftalanHotelSystem.Application.DataTransferObject;
using NaftalanHotelSystem.Domain.Entites;
using NaftalanHotelSystem.Domain.Enums;

namespace NaftalanHotelSystem.Application.Concretes.Services;

public class TreatmentMethodService : ITreatmentMethodService
{
    private readonly IUnitOfWork _unitOfWork;

    public TreatmentMethodService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task CreateTreatmentMethodAsync(TreatmentMethodCreateDto dto)
    {
        var treatment = new TreatmentMethod
        {
            Translations = dto.TreatmentMethodTranslationDtos.Select(t => new TreatmentMethodTranslation
            {
                Description = t.Description,
                Language = t.Language,
                Name = t.Name
            }).ToList()
        };

        await _unitOfWork.TreatmentMethodWriteRepository.CreateAsync(treatment);
        await _unitOfWork.SaveChangesAsync();
    }


    public async Task DeleteTreatmentMethodAsync(int id)
    {
        var treatment = _unitOfWork.TreatmentMethodReadRepository.Table
            .Include(t => t.Translations).FirstOrDefault(t => t.Id == id);

        _unitOfWork.TreatmentMethodWriteRepository.Remove(treatment);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<List<TreatmentMethodDto>> GetAllTrearmentMethodAsync()
    {
        var treatments = await _unitOfWork.TreatmentMethodReadRepository.Table
            .Include(t => t.Translations).ToListAsync();

        var result = treatments
      .SelectMany(t => t.Translations.Select(tr => new TreatmentMethodDto
      {
          Id = t.Id,
          Name = tr.Name,
          Description = tr.Description,
          Language = tr.Language
      }))
      .ToList();


        return result;
    }

    public async Task<TreatmentMethodDto> GetTreatmentMethodByIdAsync(int id, Language? language = Language.Az)
    {
        var treatment = await _unitOfWork.TreatmentMethodReadRepository.Table
            .Include(t => t.Translations).FirstOrDefaultAsync(t => t.Id == id);

        var translation = language == null
            ? treatment.Translations.FirstOrDefault()
            : treatment.Translations.FirstOrDefault(t => t.Id == id);

        return new TreatmentMethodDto
        {
            Id = id,
            Description = translation.Description,
            Language = translation.Language,
            Name = translation.Name
        };

    }


    public async Task UpdateTrearmentMethodAsync(TrearmentMethodWriteDto dto)
    {
        var treatment = await _unitOfWork.TreatmentMethodWriteRepository.Table
            .Include(t => t.Translations)
            .FirstOrDefaultAsync(t => t.Id == dto.Id);

        if (treatment == null)
            throw new Exception("Treatment method not found");

        // Yeni translation-ları əvəzləyirik
        treatment.Translations = dto.TreatmentMethodTranslationDtos.Select(t => new TreatmentMethodTranslation
        {
            Name = t.Name,
            Description = t.Description,
            Language = t.Language
        }).ToList();

        _unitOfWork.TreatmentMethodWriteRepository.Update(treatment);
        await _unitOfWork.SaveChangesAsync();
    }
}


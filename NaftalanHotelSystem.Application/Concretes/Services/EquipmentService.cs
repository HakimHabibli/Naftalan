using Microsoft.EntityFrameworkCore;
using NaftalanHotelSystem.Application.Abstractions.Repositories;
using NaftalanHotelSystem.Application.Abstractions.Services;
using NaftalanHotelSystem.Application.Abstractions.UnitOfWork;
using NaftalanHotelSystem.Application.DataTransferObject;
using NaftalanHotelSystem.Domain.Entites;
using NaftalanHotelSystem.Domain.Enums;

public class EquipmentService : IEquipmentService
{
    private readonly IUnitOfWork _unitOfWork;

    public EquipmentService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<EquipmentDto>> GetAllEquipmentAsync()
    {
        var equipments = await _unitOfWork.EquipmentReadRepository.Table
            .Include(e => e.EquipmentTranslations)
            .ToListAsync();

        var result = equipments
            .SelectMany(e => e.EquipmentTranslations.Select(t => new EquipmentDto
            {
                Id = e.Id,
                Name = t.Name,
                Language = t.Language
            }))
            .ToList();

        return result;
    }

    public async Task CreateAsync(EquipmentCreateDto dto)
    {
        var equipment = new Equipment
        {
            EquipmentTranslations = dto.Translations.Select(t => new EquipmentTranslation
            {
                Name = t.Name,
                Language = t.Language
            }).ToList()
        };

        await _unitOfWork.EquipmentWriteRepository.CreateAsync(equipment);
    }


    //TODO : Butun getbyid servislerinde sadece idye gore olmalidi servis daxilinde ise butun translationlarini getirmey lazimdi  
    //    public async Task<EquipmentDto> GetEquipmentByIdAsync(int id)  -- >Bele olmalidi 

    public async Task<EquipmentDto> GetEquipmentByIdAsync(int id, Language? language = Language.Az)
    {
        var equipment = await _unitOfWork.EquipmentReadRepository.Table
            .Include(e => e.EquipmentTranslations)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (equipment == null)
            return null;

        var translation = language == null
            ? equipment.EquipmentTranslations.FirstOrDefault()
            : equipment.EquipmentTranslations.FirstOrDefault(t => t.Language == language);

        return new EquipmentDto
        {
            Id = equipment.Id,
            Name = translation?.Name ?? string.Empty,
            Language = translation?.Language ?? default
        };
    }

    public async Task DeleteEquipmentAsync(int id)
    {
        var equipment = await _unitOfWork.EquipmentWriteRepository.Table.Include(e => e.EquipmentTranslations).FirstOrDefaultAsync(e => e.Id == id);
         _unitOfWork.EquipmentWriteRepository.Remove(equipment);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateEquipmentAsync(int id,EquipmentUpdateDto dto)
    {
       
        var equipment = await _unitOfWork.EquipmentWriteRepository.Table.
            Include(e => e.EquipmentTranslations).FirstOrDefaultAsync(e => e.Id == id);

        equipment.EquipmentTranslations = dto.Translations.Select(t => new EquipmentTranslation
        {
            Name = t.Name,
            Language = t.Language,
        }).ToList();

         _unitOfWork.EquipmentWriteRepository.Update(equipment);
        await _unitOfWork.SaveChangesAsync();
    }

}

using NaftalanHotelSystem.Application.DataTransferObject.Equipment;
using NaftalanHotelSystem.Domain.Enums;
using Org.BouncyCastle.Asn1.Mozilla;

namespace NaftalanHotelSystem.Application.Abstractions.Services;

public interface IEquipmentService 
{
    public Task<List<EquipmentDto>> GetAllEquipmentAsync();
    public Task<EquipmentGetDto> GetEquipmentByIdAsync(int id);
    public Task CreateAsync(EquipmentCreateDto dto);
    public Task DeleteEquipmentAsync(int id);
    public Task UpdateEquipmentAsync(int id,EquipmentUpdateDto equipment);
}


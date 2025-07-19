using NaftalanHotelSystem.Application.DataTransferObject.Illness;
using NaftalanHotelSystem.Domain.Enums;

namespace NaftalanHotelSystem.Application.Abstractions.Services;

public interface IIllnessService
{
    Task<int> CreateIllnessAsync(IllnessCreateDto dto);
    Task UpdateIllnessAsync(int id, IllnessUpdateDto dto);
    Task DeleteIllnessAsync(int id);
    Task<List<IllnessGetDto>> GetAllIllnessesAsync();
    Task<IllnessGetDto> GetIllnessByIdAsync(int id);
}

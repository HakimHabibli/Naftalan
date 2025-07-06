using NaftalanHotelSystem.Application.DataTransferObject;
using NaftalanHotelSystem.Domain.Enums;

namespace NaftalanHotelSystem.Application.Abstractions.Services;

public interface IIllnessService
{
    Task<int> CreateIllnessAsync(IllnessCreateDto dto);
    Task UpdateIllnessAsync(int id, IllnessUpdateDto dto);
    Task DeleteIllnessAsync(int id);
    Task<List<IllnessDto>> GetAllIllnessesAsync();
    Task<IllnessDto> GetIllnessByIdAsync(int id, Language? language = Language.Az);
}

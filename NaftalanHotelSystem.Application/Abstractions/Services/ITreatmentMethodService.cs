using NaftalanHotelSystem.Application.DataTransferObject;
using NaftalanHotelSystem.Domain.Enums;

namespace NaftalanHotelSystem.Application.Abstractions.Services;

public interface ITreatmentMethodService 
{
    public Task<List<TreatmentMethodDto>> GetAllTrearmentMethodAsync();
    public Task<TreatmentMethodDto> GetTreatmentMethodByIdAsync(int id ,Language? language = Language.Az);
    public Task CreateTreatmentMethodAsync(TrearmentMethodCreateDto dto);
    public Task DeleteTreatmentMethodAsync(int id);
    public Task UpdateTrearmentMethodAsync(TrearmentMethodWriteDto dto);
        
}


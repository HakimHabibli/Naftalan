using NaftalanHotelSystem.Application.DataTransferObject;
using NaftalanHotelSystem.Domain.Enums;

namespace NaftalanHotelSystem.Application.Abstractions.Services;

public interface ITreatmentMethodService 
{
    public Task<List<TreatmentMethodDto>> GetAllTrearmentMethodAsync();
    public Task<TreatmentMethodGetByIdDto> GetTreatmentMethodByIdAsync(int id );
    public Task CreateTreatmentMethodAsync(TreatmentMethodCreateDto dto);
    public Task DeleteTreatmentMethodAsync(int id);
    public Task UpdateTrearmentMethodAsync(TrearmentMethodWriteDto dto);
        
}


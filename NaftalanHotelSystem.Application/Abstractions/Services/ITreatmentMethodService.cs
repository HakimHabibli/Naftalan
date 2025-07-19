using NaftalanHotelSystem.Application.DataTransferObject.TreatmentMethod;
using NaftalanHotelSystem.Domain.Enums;

namespace NaftalanHotelSystem.Application.Abstractions.Services;

public interface ITreatmentMethodService 
{
    Task<List<TreatmentMethodGetByIdDto>> GetAllTreatmentMethodsAsync(); 
    Task<TreatmentMethodGetByIdDto> GetTreatmentMethodByIdAsync(int id);
    Task CreateTreatmentMethodAsync(TreatmentMethodCreateDto dto);
    Task DeleteTreatmentMethodAsync(int id);
    Task UpdateTreatmentMethodAsync(int id, TreatmentMethodUpdateDto dto); 

}


using NaftalanHotelSystem.Application.DataTransferObject;
using NaftalanHotelSystem.Domain.Enums;

namespace NaftalanHotelSystem.Application.Abstractions.Services;

public interface ITreatmentCategoryService 
{
    Task<TreatmentCategoryDto> GetTreatmentCategoryByIdAsync(int id, Language? language = Language.Az);
    Task<List<TreatmentCategoryDto>> GetAllTreatmentCategoriesAsync();
    Task DeleteTreatmentCategoryAsync(int id);
    Task UpdateTreatmentCategoryAsync(int id, TreatmentCategoryUpdateDto dto);
    Task<int> CreateTreatmentCategoryAsync(TreatmentCategoryCreateDto dto);

}

using NaftalanHotelSystem.Application.DataTransferObject.Child;

namespace NaftalanHotelSystem.Application.Abstractions.Services;

public interface IChildService 
{
    public Task<ChildGetDto> CreateChildAsync(ChildCreateDto dto);
    public Task<ChildGetDto> UpdateChildAsync(ChildUpdateDto dto);
    public Task DeleteChildAsync(int id);
    public Task<ChildGetDto> GetByIdChildAsync(int id); 
    public Task<List<ChildGetDto>> GetAllChildAsync ();
}

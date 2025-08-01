using NaftalanHotelSystem.Application.DataTransferObject.Room;
using NaftalanHotelSystem.Domain.Entites;

namespace NaftalanHotelSystem.Application.Abstractions.Services;

public interface IRoomService
{
    Task<List<RoomGetDto>> GetAllRoomsAsync();
    Task<RoomGetDto> GetRoomByIdAsync(int id);
    Task CreateRoomAsync(RoomCreateDto dto);
    Task DeleteRoomAsync(int id);
    Task UpdateRoomAsync(int id, RoomUpdateDto dto); 
}
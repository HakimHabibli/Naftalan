using NaftalanHotelSystem.Application.DataTransferObject;
using NaftalanHotelSystem.Domain.Entites;

namespace NaftalanHotelSystem.Application.Abstractions.Services;

public interface IRoomService
{

    public Task<List<RoomGetDto>> GetAllRoomsAsync();

    public Task<RoomCreateDto> GetRoomByIdAsync(int id);

    public Task CreateRoomAsync(RoomCreateDto room);
    public Task UpdateRoomAsync(int id ,RoomCreateDto room);
    public Task DeleteRoomAsync(int id);

}

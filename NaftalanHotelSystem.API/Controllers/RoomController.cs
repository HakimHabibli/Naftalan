using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NaftalanHotelSystem.Application.Abstractions.Services;
using NaftalanHotelSystem.Application.DataTransferObject;

namespace NaftalanHotelSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoomController : ControllerBase
{
    private readonly IRoomService _roomService;

    public RoomController(IRoomService roomService)
    {
        _roomService = roomService;
    }

    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RoomCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _roomService.CreateRoomAsync(dto);
        return Ok();
    }

   
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var rooms = await _roomService.GetAllRoomsAsync(); 
        return Ok(rooms);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var room = await _roomService.GetRoomByIdAsync(id);
        if (room == null)
            return NotFound();

        return Ok(room);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] RoomCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _roomService.UpdateRoomAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _roomService.DeleteRoomAsync(id);
        return NoContent();
    }

}

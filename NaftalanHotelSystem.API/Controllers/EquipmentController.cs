using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NaftalanHotelSystem.Application.Abstractions.Services;
using NaftalanHotelSystem.Application.DataTransferObject.Equipment;
using NaftalanHotelSystem.Domain.Enums;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NaftalanHotelSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EquipmentController : ControllerBase
{
    private readonly IEquipmentService _equipmentService;

    public EquipmentController(IEquipmentService equipmentService)
    {
        _equipmentService = equipmentService;
    }


    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        var equipments = await _equipmentService.GetAllEquipmentAsync();
        return Ok(equipments);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(int id,Language? language)
    {
        var equipment = await _equipmentService.GetEquipmentByIdAsync(id,language);

        if (equipment == null)
            return NotFound();
        
        return Ok(equipment);
    }
 
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] EquipmentCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _equipmentService.CreateAsync(dto);

        return Ok();
    }


    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id,[FromBody] EquipmentUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _equipmentService.UpdateEquipmentAsync(id,dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _equipmentService.DeleteEquipmentAsync(id);
        return NoContent();
    }
}

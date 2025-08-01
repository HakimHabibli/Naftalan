using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NaftalanHotelSystem.Application.Abstractions.Services;
using NaftalanHotelSystem.Application.DataTransferObject.Child;

namespace NaftalanHotelSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChildrenController : ControllerBase
{
    private readonly IChildService _childService;

    public ChildrenController(IChildService childService)
    {
        _childService = childService;
    }

    [HttpPost]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(ChildCreateDto dto)
    {

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }


        var createdChild = await _childService.CreateChildAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = createdChild.Id }, createdChild);
    }

    [HttpPut("{id}")]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, ChildUpdateDto dto)
    {
        if (id != dto.Id)
        {
            return BadRequest("ID mismatch.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var updatedChild = await _childService.UpdateChildAsync(dto);
        return Ok(updatedChild);
    }


    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var child = await _childService.GetByIdChildAsync(id);
            return Ok(child);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        var children = await _childService.GetAllChildAsync();
        return Ok(children);
    }

    [HttpDelete("{id}")]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _childService.DeleteChildAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}
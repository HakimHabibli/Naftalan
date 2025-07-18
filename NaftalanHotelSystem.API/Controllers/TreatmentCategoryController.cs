using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NaftalanHotelSystem.Application.Abstractions.Services;
using NaftalanHotelSystem.Application.DataTransferObject;
using NaftalanHotelSystem.Domain.Enums;

[ApiController]
[Route("api/[controller]")]
public class TreatmentCategoryController : ControllerBase
{
    private readonly ITreatmentCategoryService _service;

    public TreatmentCategoryController(ITreatmentCategoryService service)
    {
        _service = service;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllTreatmentCategoriesAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(int id, [FromQuery] Language? language)
    {
        var result = await _service.GetTreatmentCategoryByIdAsync(id, language);
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] TreatmentCategoryCreateDto dto)
    {
        await _service.CreateTreatmentCategoryAsync(dto);
        return Ok();
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] TreatmentCategoryUpdateDto dto)
    {
        await _service.UpdateTreatmentCategoryAsync(id, dto);
        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteTreatmentCategoryAsync(id);
        return Ok();
    }
}

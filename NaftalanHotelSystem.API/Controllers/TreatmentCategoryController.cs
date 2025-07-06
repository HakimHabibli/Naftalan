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
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllTreatmentCategoriesAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, [FromQuery] Language? language)
    {
        var result = await _service.GetTreatmentCategoryByIdAsync(id, language);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TreatmentCategoryCreateDto dto)
    {
        await _service.CreateTreatmentCategoryAsync(dto);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] TreatmentCategoryUpdateDto dto)
    {
        await _service.UpdateTreatmentCategoryAsync(id, dto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteTreatmentCategoryAsync(id);
        return Ok();
    }
}

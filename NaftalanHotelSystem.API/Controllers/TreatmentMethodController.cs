using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NaftalanHotelSystem.Application.Abstractions.Services;
using NaftalanHotelSystem.Application.DataTransferObject;
using NaftalanHotelSystem.Domain.Enums;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NaftalanHotelSystem.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TreatmentMethodController : ControllerBase
    {
        private readonly ITreatmentMethodService _treatmentService;

        public TreatmentMethodController(ITreatmentMethodService treatmentService)
        {
            _treatmentService = treatmentService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] TreatmentMethodCreateDto dto)
        {
            await _treatmentService.CreateTreatmentMethodAsync(dto);
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] TrearmentMethodWriteDto dto)
        {
            await _treatmentService.UpdateTrearmentMethodAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _treatmentService.DeleteTreatmentMethodAsync(id);
            return NoContent();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var result = await _treatmentService.GetAllTrearmentMethodAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _treatmentService.GetTreatmentMethodByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NaftalanHotelSystem.Application.Abstractions.Services;
using NaftalanHotelSystem.Application.DataTransferObject;
using NaftalanHotelSystem.Application.DataTransferObject.TreatmentMethod;
using NaftalanHotelSystem.Domain.Enums;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860


namespace NaftalanHotelSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreatmentMethodController : ControllerBase
    {
        private readonly ITreatmentMethodService _treatmentMethodService;

        public TreatmentMethodController(ITreatmentMethodService treatmentMethodService)
        {
            _treatmentMethodService = treatmentMethodService;
        }

      
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] TreatmentMethodCreateDto dto) 
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _treatmentMethodService.CreateTreatmentMethodAsync(dto);
                return StatusCode(201, "Treatment Method created successfully.");
            }
            catch (Exception ex)
            {
               
                return BadRequest(ex.Message);
            }
        }

   
        [HttpPut("{id}")] 
        [Authorize(Roles = "Admin")]
        [Consumes("multipart/form-data")] 
        public async Task<IActionResult> Update(int id, [FromForm] TreatmentMethodUpdateDto dto)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _treatmentMethodService.UpdateTreatmentMethodAsync(id, dto);
                return Ok("Treatment Method updated successfully."); 
            }
            catch (Exception ex)
            {
               
                return NotFound(ex.Message); 
            }
        }

    
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _treatmentMethodService.DeleteTreatmentMethodAsync(id);
                return NoContent(); 
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
            var result = await _treatmentMethodService.GetAllTreatmentMethodsAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _treatmentMethodService.GetTreatmentMethodByIdAsync(id);
            if (result == null)
            {
                return NotFound($"Treatment Method with Id {id} not found."); 
            }
            return Ok(result);
        }
    }
}

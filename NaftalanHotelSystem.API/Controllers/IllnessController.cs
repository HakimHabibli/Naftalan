using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NaftalanHotelSystem.Application.Abstractions.Services;
using NaftalanHotelSystem.Application.DataTransferObject.Illness;
using NaftalanHotelSystem.Domain.Enums;

namespace NaftalanHotelSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IllnessController : ControllerBase
    {
        private readonly IIllnessService _illnessService;

        public IllnessController(IIllnessService illnessService)
        {
            _illnessService = illnessService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var result = await _illnessService.GetAllIllnessesAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _illnessService.GetIllnessByIdAsync(id);
            if (result == null)
            {
                return NotFound($"Illness with Id {id} not found."); // Əgər servis null qaytarsa
            }
            return Ok(result);
        }

        // --- CREATE ENDPOINT (Şəkil yükləmə dəstəyi ilə) ---
        [HttpPost]
        [Authorize(Roles = "Admin")]
        // Fayl yükləmə zamanı [FromForm] istifadə olunur
        public async Task<IActionResult> Create([FromForm] IllnessCreateDto dto)
        {
            // Model Validasiyası
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Əgər şəkil faylı yoxdursa və ya boşdursa, xəta qaytara bilərsiniz
            // Ya da servisdə bu halı idarə edə bilərsiniz.
            // if (dto.ImageFile == null || dto.ImageFile.Length == 0)
            // {
            //     return BadRequest("Image file is required.");
            // }

            await _illnessService.CreateIllnessAsync(dto);
            return StatusCode(201, "Illness created successfully."); // 201 Created status kodu daha uyğundur
        }

        // --- UPDATE ENDPOINT (Şəkil yükləmə dəstəyi ilə) ---
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        // Fayl yükləmə zamanı [FromForm] istifadə olunur
        public async Task<IActionResult> Update(int id, [FromForm] IllnessUpdateDto dto)
        {
          
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _illnessService.UpdateIllnessAsync(id, dto);
                return Ok("Illness updated successfully.");
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
                await _illnessService.DeleteIllnessAsync(id);
                return NoContent(); // 204 No Content status kodu silmə üçün daha uyğundur
            }
            catch (Exception ex)
            {
                // Xəta idarəetməsi (məsələn, tapılmadı xətası)
                return NotFound(ex.Message);
            }
        }
    }
}

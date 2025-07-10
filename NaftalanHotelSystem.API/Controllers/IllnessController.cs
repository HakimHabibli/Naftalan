using Microsoft.AspNetCore.Mvc;
using NaftalanHotelSystem.Application.Abstractions.Services;
using NaftalanHotelSystem.Application.DataTransferObject;
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
        public async Task<IActionResult> GetAll()
        {
            var result = await _illnessService.GetAllIllnessesAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, [FromQuery] Language? language)
        {
            var result = await _illnessService.GetIllnessByIdAsync(id, language);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] IllnessCreateDto dto)
        {
            await _illnessService.CreateIllnessAsync(dto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] IllnessUpdateDto dto)
        {
            await _illnessService.UpdateIllnessAsync(id, dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _illnessService.DeleteIllnessAsync(id);
            return Ok();
        }
    }
}

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NaftalanHotelSystem.Application.Abstractions.Services;
using NaftalanHotelSystem.Application.Concretes.Services;

namespace NaftalanHotelSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutController : ControllerBase
    {
        private readonly IAboutService _aboutService;

        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var about = await _aboutService.GetAboutAsync();
            if (about == null) return NotFound();
            return Ok(about);
            
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] AboutUpdateDto dto)
        {
            await _aboutService.UpdateAboutAsync(dto);
            return NoContent(); 
        }
    }
}

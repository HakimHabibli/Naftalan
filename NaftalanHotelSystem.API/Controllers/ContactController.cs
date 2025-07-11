using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NaftalanHotelSystem.Application.Abstractions.Services;
using NaftalanHotelSystem.Application.DataTransferObject;

namespace NaftalanHotelSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var contact = await _contactService.GetContactAsync();
            if (contact == null) return NotFound();
            return Ok(contact);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ContactUpdateDto dto)
        {
            await _contactService.UpdateContactAsync(dto);
            return NoContent();
        }
    }
}

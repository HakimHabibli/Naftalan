using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NaftalanHotelSystem.API.ModelBinders;
using NaftalanHotelSystem.Application.Abstractions.Services;
using NaftalanHotelSystem.Application.DataTransferObject;
using NaftalanHotelSystem.Application.DataTransferObject.About;
using Newtonsoft.Json;

namespace NaftalanHotelSystem.API.Controllers;

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
    [AllowAnonymous]
    public async Task<IActionResult> Get()
    {
        var about = await _aboutService.GetAboutAsync();
        if (about == null) return NotFound();
        return Ok(about);
        
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update([FromForm][ModelBinder(BinderType = typeof(AboutUpdateDtoModelBinder))] BinableAboutUpdateDto dto)
    {
     

        List<AboutTranslationUpdateDto> translations;
        try
        {
            translations = JsonConvert.DeserializeObject<List<AboutTranslationUpdateDto>>(dto.Translations);
        }
        catch (Exception ex)
        {
            return BadRequest("Translations JSON is invalid: " + ex.Message);
        }

        var appDto = new AboutUpdateDto
        {
            Id = dto.Id,
            VideoLink = dto.VideoLink,
            ImageFile = dto.ImageFile,
            Translations = translations
        };

        await _aboutService.UpdateAboutAsync(appDto);
        return NoContent();
    }
}

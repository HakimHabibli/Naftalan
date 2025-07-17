using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using NaftalanHotelSystem.Application.Abstractions.Services;
using NaftalanHotelSystem.Application.DataTransferObject.Package;
using NaftalanHotelSystem.Domain.Enums;

namespace NaftalanHotelSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PackageController : ControllerBase
{
    private readonly IPackageService _packageService;
    public PackageController(IPackageService packageService)
    {
        _packageService = packageService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var packages = await _packageService.GetAllPackageAsync();
        return Ok(packages);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, Language? language)
    {
        var package = await _packageService.GetPackageByIdAsync(id, language);
        if (package == null)
            return NotFound();

        return Ok(package);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PackageCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _packageService.CreatePackageAsync(dto);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] PackageCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _packageService.UpdatePackageAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _packageService.DeletePackageAsync(id);
        return NoContent();
    }
}
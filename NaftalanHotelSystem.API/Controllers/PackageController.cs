using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Authorization;
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
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        var packages = await _packageService.GetAllPackageAsync();
        return Ok(packages);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(int id)
    {
        var package = await _packageService.GetPackageByIdAsync(id);
        if (package == null)
            return NotFound();

        return Ok(package);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] PackageCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _packageService.CreatePackageAsync(dto);
        return Ok();
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] PackageCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _packageService.UpdatePackageAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _packageService.DeletePackageAsync(id);
        return NoContent();
    }
}
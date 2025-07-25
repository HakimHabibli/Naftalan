﻿using Microsoft.AspNetCore.Authorization;
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
                return NotFound($"Illness with Id {id} not found."); 
            }
            return Ok(result);
        }

       
        [HttpPost]
        [Authorize(Roles = "Admin")]
       
        public async Task<IActionResult> Create([FromForm] IllnessCreateDto dto)
        {
         
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

        

            await _illnessService.CreateIllnessAsync(dto);
            return StatusCode(201, "Illness created successfully."); 
        }

       
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
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
                return NoContent(); 
            }
            catch (Exception ex)
            {
                
                return NotFound(ex.Message);
            }
        }
    }
}

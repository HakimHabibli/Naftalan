﻿using Microsoft.AspNetCore.Http;
using NaftalanHotelSystem.Application.DataTransferObject.About;
using NaftalanHotelSystem.Domain.Entites;

namespace NaftalanHotelSystem.Application.Abstractions.Services;

public interface IAboutService 
{
    public Task<AboutDto> GetAboutAsync();
    public Task UpdateAboutAsync(AboutUpdateDto dto);
}

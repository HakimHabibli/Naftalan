﻿using Microsoft.AspNetCore.Http;

namespace NaftalanHotelSystem.Application.DataTransferObject.Illness;

public class IllnessCreateDto
{
    public int TreatmentCategoryId { get; set; }
    public List<IllnessTranslationCreateDto> Translations { get; set; }
    public IFormFile ImageFile { get; set; }
}
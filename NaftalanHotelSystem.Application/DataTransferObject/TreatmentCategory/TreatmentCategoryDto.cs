namespace NaftalanHotelSystem.Application.DataTransferObject;

public class TreatmentCategoryDto
{
    public int Id { get; set; }
    public List<TreatmentCategoryTranslationDto> Translations { get; set; }
}

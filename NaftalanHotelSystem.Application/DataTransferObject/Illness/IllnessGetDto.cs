namespace NaftalanHotelSystem.Application.DataTransferObject.Illness;

public class IllnessGetDto
{
    public int Id { get; set; }
    public int TreatmentCategoryId { get; set; }
    public List<IllnessTranslationDto> Translations { get; set; }
    public List<TreatmentCategoryTranslationDto> TreatmentCategoryTranslations { get; set; }
    public string ImageUrls { get; set; }
}
namespace NaftalanHotelSystem.Application.DataTransferObject.Illness;

public class IllnessCreateDto
{
    public int TreatmentCategoryId { get; set; }
    public List<IllnessTranslationCreateDto> Translations { get; set; }
}

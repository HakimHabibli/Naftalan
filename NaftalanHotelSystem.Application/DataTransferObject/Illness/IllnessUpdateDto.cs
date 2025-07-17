namespace NaftalanHotelSystem.Application.DataTransferObject.Illness;

public class IllnessUpdateDto
{
    public int TreatmentCategoryId { get; set; }
    public List<IllnessTranslationUpdateDto> Translations { get; set; }
}

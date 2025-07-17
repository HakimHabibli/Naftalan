namespace NaftalanHotelSystem.Application.DataTransferObject.Illness;

public class IllnessDto
{
    public int Id { get; set; }
    public int TreatmentCategoryId { get; set; }
    public List<IllnessTranslationDto> Translations { get; set; }
}

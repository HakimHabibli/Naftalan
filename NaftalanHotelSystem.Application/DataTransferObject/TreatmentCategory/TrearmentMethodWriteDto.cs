namespace NaftalanHotelSystem.Application.DataTransferObject;

public class TrearmentMethodWriteDto//Update
{
    public int Id { get; set; }
    public List<TreatmentMethodTranslationDto> TreatmentMethodTranslationDtos { get; set; }
}
using NaftalanHotelSystem.Domain.Enums;

namespace NaftalanHotelSystem.Application.DataTransferObject;

public class TreatmentMethodDto //Getid,GetAll
{
    public int Id { get; set; }
    public string Name { get; set; }    
    public Language Language { get; set; }
    public string Description { get; set; }
}

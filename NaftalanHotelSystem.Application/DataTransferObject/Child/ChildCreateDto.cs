using NaftalanHotelSystem.Application.DataTransferObject.Common;

namespace NaftalanHotelSystem.Application.DataTransferObject.Child;

public class ChildCreateDto
{
    public string AgeRange { get; set; }
    public bool HasTreatment { get; set; }
    public double Price { get; set; }

}
public class ChildUpdateDto : BaseDto 
{
    public string AgeRange { get; set; }
    public bool HasTreatment { get; set; }
    public double Price { get; set; }
}
public class ChildDeleteDto : BaseDto 
{ }

public class ChildGetDto : BaseDto 
{
    public string AgeRange { get; set; }
    public bool HasTreatment { get; set; }
    public double Price { get; set; }
}
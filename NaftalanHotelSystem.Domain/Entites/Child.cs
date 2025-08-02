using NaftalanHotelSystem.Domain.Common;

namespace NaftalanHotelSystem.Domain.Entites;

public class Child : BaseEntity
    {
        public string AgeRange { get; set; }
        public bool HasTreatment { get; set; }
        public double Price { get; set; }


        public int RoomId { get; set; }
    public ICollection<RoomChild> RoomChildren { get; set; } = new List<RoomChild>();
}
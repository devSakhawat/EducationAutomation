namespace pbERP.Domain.DTOs.FEducation;

public class FEduBClassOrHallRoomDto
{
   public long ClassRoomId { get; set; }

   public long? BuildingId { get; set; }

   public string ClassRoomName { get; set; }

   public int? AccomodationCapacity { get; set; }

   public string BuildingName { get; set; }

   //public virtual ICollection<FEduCClassOrHall> FEduCClassOrHalls { get; set; } = new List<FEduCClassOrHall>();

   //public virtual ICollection<FEduDStudentAllocateHallSeat> FEduDStudentAllocateHallSeats { get; set; } = new List<FEduDStudentAllocateHallSeat>();
}


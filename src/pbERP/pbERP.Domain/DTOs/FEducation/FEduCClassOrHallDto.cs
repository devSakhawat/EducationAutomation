namespace pbERP.Domain.DTOs.FEducation;

public class FEduCClassOrHallDto
{
   public long HallSeatId { get; set; }

   public int? HallSeatNumber { get; set; }

   public double? HallSeatCharge { get; set; }

   public long? ClassRoomId { get; set; }

   public string ClassRoomName { get; set; }

   //public virtual ICollection<FEduDStudentAllocateHallSeat> FEduDStudentAllocateHallSeats { get; set; } = new List<FEduDStudentAllocateHallSeat>();
}

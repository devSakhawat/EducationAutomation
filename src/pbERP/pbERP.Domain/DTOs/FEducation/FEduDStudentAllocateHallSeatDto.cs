
namespace pbERP.Domain.DTOs.FEducation;

public class FEduDStudentAllocateHallSeatDto
{
   public long AllocateStudentHallSeatId { get; set; }

   public long? StudentId { get; set; }

   public long? ClassRoomId { get; set; }

   public long? HallSeatId { get; set; }

   public string ClassRoomName { get; set; }

   public int? HallSeatNumber { get; set; }

   public string StudentName { get; set; }

   //public virtual FEduBClassOrHallRoom ClassRoom { get; set; }

   //public virtual FEduCClassOrHall HallSeat { get; set; }

   //public virtual FEduAStudent Student { get; set; }
}

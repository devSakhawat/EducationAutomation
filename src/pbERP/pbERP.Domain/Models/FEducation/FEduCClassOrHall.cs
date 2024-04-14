using System;
using System.Collections.Generic;

namespace pbERP.Domain.Models.FEducation;

public partial class FEduCClassOrHall
{
    public long HallSeatId { get; set; }

    public int? HallSeatNumber { get; set; }

    public double? HallSeatCharge { get; set; }

    public long? ClassRoomId { get; set; }

    public virtual FEduBClassOrHallRoom ClassRoom { get; set; }

    public virtual ICollection<FEduDStudentAllocateHallSeat> FEduDStudentAllocateHallSeats { get; set; } = new List<FEduDStudentAllocateHallSeat>();
}

using System;
using System.Collections.Generic;

namespace pbERP.Domain.Models.FEducation;

public partial class FEduDStudentAllocateHallSeat
{
    public long AllocateStudentHallSeatId { get; set; }

    public long? StudentId { get; set; }

    public long? ClassRoomId { get; set; }

    public long? HallSeatId { get; set; }

    public virtual FEduBClassOrHallRoom ClassRoom { get; set; }

    public virtual FEduCClassOrHall HallSeat { get; set; }
}

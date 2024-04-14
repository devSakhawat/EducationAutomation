using System;
using System.Collections.Generic;

namespace pbERP.Domain.Models.FEducation;

public partial class FEduBClassOrHallRoom
{
    public long ClassRoomId { get; set; }

    public long? BuildingId { get; set; }

    public string ClassRoomName { get; set; }

    public string? ClassRoomNameLocal { get; set; }

    public int? AccomodationCapacity { get; set; }

    public virtual FEduBBuilding Building { get; set; }

    public virtual ICollection<FEduCClassOrHall> FEduCClassOrHalls { get; set; } = new List<FEduCClassOrHall>();

    public virtual ICollection<FEduDStudentAllocateHallSeat> FEduDStudentAllocateHallSeats { get; set; } = new List<FEduDStudentAllocateHallSeat>();
}

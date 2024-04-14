using System;
using System.Collections.Generic;

namespace pbERP.Domain.Models.FEducation;

public partial class FEduLClassShift
{
    public long ClassShiftId { get; set; }

    public string ClassShiftName { get; set; }

    public string ClassShiftNameLocal { get; set; }

   public virtual ICollection<FEduPLinkClassShift> FEduPLinkClassShifts { get; set; } = new List<FEduPLinkClassShift>();
}

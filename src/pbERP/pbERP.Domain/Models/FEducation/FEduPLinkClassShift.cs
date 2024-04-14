using System;
using System.Collections.Generic;

namespace pbERP.Domain.Models.FEducation;

public partial class FEduPLinkClassShift
{
    public long LinkClassShiftId { get; set; }

    public long? ClassId { get; set; }

    public long? ClassShiftId { get; set; }

    public virtual FEduAClass Class { get; set; }

    public virtual FEduLClassShift ClassShift { get; set; }
}

using System;
using System.Collections.Generic;

namespace pbERP.Domain.Models.FEducation;

public partial class FEduUClassPeriod
{
    public long ClassPeriodId { get; set; }

    public long? ClassPeriodSl { get; set; }

    public string ClassPeriodName { get; set; }

    public string ClassPeriodNameInLocal { get; set; }
}

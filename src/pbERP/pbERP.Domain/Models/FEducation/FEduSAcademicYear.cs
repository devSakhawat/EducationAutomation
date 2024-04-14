using System;
using System.Collections.Generic;

namespace pbERP.Domain.Models.FEducation;

public partial class FEduSAcademicYear
{
    public long AcademicYearId { get; set; }

    public string AcademicYear { get; set; }

    public string AcademicYearInLocal { get; set; }
}

using System;
using System.Collections.Generic;

namespace pbERP.Domain.Models.FEducation;

public partial class FEduTAcademicSession
{
    public long AcademicSessionId { get; set; }

    public string AcademicSession { get; set; }

    public string AcademicSessionInLocal { get; set; }
}

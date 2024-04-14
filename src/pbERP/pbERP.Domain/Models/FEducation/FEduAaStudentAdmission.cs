using System;
using System.Collections.Generic;

namespace pbERP.Domain.Models.FEducation;

public partial class FEduAaStudentAdmission
{
    public long StudAdmId { get; set; }

    public long? StudentId { get; set; }

    public DateTime? StudAdmDate { get; set; }

    public long? StudAdmClassId { get; set; }

    public long? StudAdmClassRoll { get; set; }
}

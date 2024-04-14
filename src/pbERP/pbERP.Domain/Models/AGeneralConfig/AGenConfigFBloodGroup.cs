using System;
using System.Collections.Generic;
using pbERP.Domain.Models.DHR;
using pbERP.Domain.Models.FEducation;

namespace pbERP.Domain.Models.AGeneralConfig;

public partial class AGenConfigFBloodGroup
{
    public long BloodGroupId { get; set; }

    public string BloodGroupName { get; set; }

    public string BloodGroupNameLocal { get; set; }

    public virtual ICollection<DHrJEmployee> DHrJEmployees { get; set; } = new List<DHrJEmployee>();

    public virtual ICollection<FEduAStudent> FEduAStudents { get; set; } = new List<FEduAStudent>();
}

using System;
using System.Collections.Generic;
using pbERP.Domain.Models.DHR;
using pbERP.Domain.Models.FEducation;

namespace pbERP.Domain.Models.AGeneralConfig;

public partial class AGenConfigGReligion
{
    public long ReligionId { get; set; }

    public string ReligionName { get; set; }

    public string ReligionNameLocal { get; set; }

    public virtual ICollection<DHrJEmployee> DHrJEmployees { get; set; } = new List<DHrJEmployee>();

    public virtual ICollection<FEduAStudent> FEduAStudents { get; set; } = new List<FEduAStudent>();
}

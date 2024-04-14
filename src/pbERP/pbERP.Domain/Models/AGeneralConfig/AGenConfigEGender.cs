using System;
using System.Collections.Generic;
using pbERP.Domain.Models.DHR;
using pbERP.Domain.Models.FEducation;

namespace pbERP.Domain.Models.AGeneralConfig;

public partial class AGenConfigEGender
{
    public long GenderId { get; set; }

    public string GenderName { get; set; }

    public string GenderNameLocal { get; set; }

    public virtual ICollection<DHrJEmployee> DHrJEmployees { get; set; } = new List<DHrJEmployee>();

    public virtual ICollection<FEduAStudent> FEduAStudents { get; set; } = new List<FEduAStudent>();
}

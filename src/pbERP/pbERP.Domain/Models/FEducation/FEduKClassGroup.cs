using System;
using System.Collections.Generic;

namespace pbERP.Domain.Models.FEducation;

public partial class FEduKClassGroup
{
    public long ClassGroupId { get; set; }

    public string ClassGroupName { get; set; }

    public string ClassGroupNameLocal { get; set; }

    public virtual ICollection<FEduNLinkClassGroup> FEduNLinkClassGroups { get; set; } = new List<FEduNLinkClassGroup>();

    public virtual ICollection<FEduQLinkClassSubject> FEduQLinkClassSubjects { get; set; } = new List<FEduQLinkClassSubject>();
}

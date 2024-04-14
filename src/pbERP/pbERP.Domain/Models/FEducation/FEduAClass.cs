using System;
using System.Collections.Generic;

namespace pbERP.Domain.Models.FEducation;

public partial class FEduAClass
{
    public long ClassId { get; set; }

    public string ClassName { get; set; }

    public string ClassNameLocal { get; set; }

    public virtual ICollection<FEduNLinkClassGroup> FEduNLinkClassGroups { get; set; } = new List<FEduNLinkClassGroup>();

    public virtual ICollection<FEduOLinkClassSection> FEduOLinkClassSections { get; set; } = new List<FEduOLinkClassSection>();

    public virtual ICollection<FEduPLinkClassShift> FEduPLinkClassShifts { get; set; } = new List<FEduPLinkClassShift>();

    public virtual ICollection<FEduQLinkClassSubject> FEduQLinkClassSubjects { get; set; } = new List<FEduQLinkClassSubject>();
}

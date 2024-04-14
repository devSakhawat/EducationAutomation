using System;
using System.Collections.Generic;

namespace pbERP.Domain.Models.FEducation;

public partial class FEduMClassSubject
{
    public long ClassSubjectId { get; set; }

    public int? ClassSubjectSl { get; set; }

    public string ClassSubjectCode { get; set; }

    public string ClassSubjectName { get; set; }

    public string ClassSubjectNameLocal { get; set; }

    public virtual ICollection<FEduQLinkClassSubject> FEduQLinkClassSubjects { get; set; } = new List<FEduQLinkClassSubject>();
}

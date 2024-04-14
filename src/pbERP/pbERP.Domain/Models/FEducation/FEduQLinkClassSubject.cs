using System;
using System.Collections.Generic;

namespace pbERP.Domain.Models.FEducation;

public partial class FEduQLinkClassSubject
{
    public long LinkClassSubjectId { get; set; }

    public long? ClassId { get; set; }

    public long? ClassGroupId { get; set; }

    public long? ClassSubjectId { get; set; }

    public virtual FEduAClass Class { get; set; }

    public virtual FEduKClassGroup ClassGroup { get; set; }

    public virtual FEduMClassSubject ClassSubject { get; set; }
}

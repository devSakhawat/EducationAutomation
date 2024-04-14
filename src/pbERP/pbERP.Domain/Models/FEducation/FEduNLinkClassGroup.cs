using System;
using System.Collections.Generic;

namespace pbERP.Domain.Models.FEducation;

public partial class FEduNLinkClassGroup
{
    public long LinkClassGroupId { get; set; }

    public long? ClassId { get; set; }

    public long? ClassGroupId { get; set; }

    public virtual FEduAClass Class { get; set; }

    public virtual FEduKClassGroup ClassGroup { get; set; }
}

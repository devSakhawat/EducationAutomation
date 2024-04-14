using System;
using System.Collections.Generic;

namespace pbERP.Domain.Models.FEducation;

public partial class FEduOLinkClassSection
{
    public long LinkClassSectionId { get; set; }

    public long? ClassId { get; set; }

    public long? ClassSectionId { get; set; }

    public virtual FEduAClass Class { get; set; }

    public virtual FEduJClassSection ClassSection { get; set; }
}

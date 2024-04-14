using System;
using System.Collections.Generic;

namespace pbERP.Domain.Models.FEducation;

public partial class FEduJClassSection
{
    public long ClassSectionId { get; set; }

    public string ClassSectionName { get; set; }

    public string ClassSectionNameLocal { get; set; }

    public virtual ICollection<FEduOLinkClassSection> FEduOLinkClassSections { get; set; } = new List<FEduOLinkClassSection>();
}

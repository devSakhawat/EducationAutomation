using System;
using System.Collections.Generic;

namespace pbERP.Domain.Models.AGeneralConfig;

public partial class AGenConfigGFont
{
    public long FontId { get; set; }

    public string FontName { get; set; }

    public virtual ICollection<AGenConfigKBooth> AGenConfigKBooths { get; set; } = new List<AGenConfigKBooth>();
}

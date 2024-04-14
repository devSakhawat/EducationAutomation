using System;
using System.Collections.Generic;

namespace pbERP.Domain.Models.AGeneralConfig;

public partial class AGenConfigFLanguage
{
    public long LanguageId { get; set; }

    public string LanguageName { get; set; }

    public virtual ICollection<AGenConfigKBooth> AGenConfigKBooths { get; set; } = new List<AGenConfigKBooth>();
}

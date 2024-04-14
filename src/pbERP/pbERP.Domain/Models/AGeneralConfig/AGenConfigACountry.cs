using System;
using System.Collections.Generic;

namespace pbERP.Domain.Models.AGeneralConfig;

public partial class AGenConfigACountry
{
    public long CountryId { get; set; }

    public string CountryName { get; set; }

    public string CountryNameLocal { get; set; }

    public virtual ICollection<AGenConfigBDivisionOrState> AGenConfigBDivisionOrStates { get; set; } = new List<AGenConfigBDivisionOrState>();
}

using System;
using System.Collections.Generic;

namespace pbERP.Domain.Models.AGeneralConfig;

public partial class AGenConfigCDistrictOrCity
{
    public long DistrictId { get; set; }

    public string DistrictName { get; set; }

    public string DistrictNameLocal { get; set; }

    public long? DivisionId { get; set; }

    public virtual ICollection<AGenConfigDPoliceStation> AGenConfigDPoliceStations { get; set; } = new List<AGenConfigDPoliceStation>();

    public virtual AGenConfigBDivisionOrState Division { get; set; }
}

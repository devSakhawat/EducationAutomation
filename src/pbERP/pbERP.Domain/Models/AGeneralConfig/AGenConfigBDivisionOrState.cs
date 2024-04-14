using System;
using System.Collections.Generic;

namespace pbERP.Domain.Models.AGeneralConfig;

public partial class AGenConfigBDivisionOrState
{
    public long DivisionId { get; set; }

    public string DivisionName { get; set; }

    public string DivisionNameLocal { get; set; }

    public long? CountryId { get; set; }

    public virtual ICollection<AGenConfigCDistrictOrCity> AGenConfigCDistrictOrCities { get; set; } = new List<AGenConfigCDistrictOrCity>();

    public virtual AGenConfigACountry Country { get; set; }
}

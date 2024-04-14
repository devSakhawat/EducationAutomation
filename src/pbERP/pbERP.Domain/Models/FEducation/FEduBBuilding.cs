using System;
using System.Collections.Generic;
using pbERP.Domain.Models.CCompany;

namespace pbERP.Domain.Models.FEducation;

public partial class FEduBBuilding
{
    public long BuildingId { get; set; }

    public string BuildingName { get; set; }

    public string BuildingNameLocal { get; set; }

    public string UsesType { get; set; }

    public long? CompanyId { get; set; }

    public virtual CCompACompany Company { get; set; }

    public virtual ICollection<FEduBClassOrHallRoom> FEduBClassOrHallRooms { get; set; } = new List<FEduBClassOrHallRoom>();
}

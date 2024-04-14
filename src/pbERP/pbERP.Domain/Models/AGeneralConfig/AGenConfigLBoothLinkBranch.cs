using System;
using System.Collections.Generic;
using pbERP.Domain.Models.CCompany;

namespace pbERP.Domain.Models.AGeneralConfig;

public partial class AGenConfigLBoothLinkBranch
{
    public long? BoothLinkBranchId { get; set; }

    public long? BoothId { get; set; }

    public long? BranchId { get; set; }

    public virtual AGenConfigKBooth Booth { get; set; }

    public virtual CCompBBranch Branch { get; set; }
}

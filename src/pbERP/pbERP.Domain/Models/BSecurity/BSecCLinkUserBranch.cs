using System;
using System.Collections.Generic;
using pbERP.Domain.Models.CCompany;

namespace pbERP.Domain.Models.BSecurity;

public partial class BSecCLinkUserBranch
{
    public long LinkUserBranchId { get; set; }

    public long? UserId { get; set; }

    public long? BranchId { get; set; }

    public virtual CCompBBranch Branch { get; set; }
}

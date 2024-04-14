using System;
using System.Collections.Generic;
using pbERP.Domain.Models.BSecurity;

namespace pbERP.Domain.Models.CCompany;

public partial class CCompBBranch
{
    public long BranchId { get; set; }

    public long? ComapanyId { get; set; }

    public string BranchCode { get; set; }

    public string BranchName { get; set; }

    public string BranchNameLocal { get; set; }

    public string BranchAddress { get; set; }

    public string BranchAddressLocal { get; set; }

    public long? PoliceStationId { get; set; }

    public string BranchPhone { get; set; }

    public string BranchWhatsApp { get; set; }

    public virtual ICollection<BSecCLinkUserBranch> BSecCLinkUserBranches { get; set; } = new List<BSecCLinkUserBranch>();

    public virtual CCompACompany Comapany { get; set; }
}

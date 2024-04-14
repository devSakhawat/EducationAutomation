using System;
using System.Collections.Generic;
using pbERP.Domain.Models.CCompany;

namespace pbERP.Domain.Models.AGeneralConfig;

public partial class AGenConfigJCompanyLinkModule
{
    public long CompanyLinkModuleId { get; set; }

    public long? CompanyId { get; set; }

    public long? ModuleId { get; set; }

    public virtual CCompACompany Company { get; set; }

    public virtual AGenConfigIModule Module { get; set; }
}

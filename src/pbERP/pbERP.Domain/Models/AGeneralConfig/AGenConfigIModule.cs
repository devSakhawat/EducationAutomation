using System;
using System.Collections.Generic;
using pbERP.Domain.Models.BSecurity;

namespace pbERP.Domain.Models.AGeneralConfig;

public partial class AGenConfigIModule
{
    public long ModuleId { get; set; }

    public string ModuleName { get; set; }

    public virtual ICollection<AGenConfigJCompanyLinkModule> AGenConfigJCompanyLinkModules { get; set; } = new List<AGenConfigJCompanyLinkModule>();

    public virtual ICollection<BSecDScreen> BSecDScreens { get; set; } = new List<BSecDScreen>();
}

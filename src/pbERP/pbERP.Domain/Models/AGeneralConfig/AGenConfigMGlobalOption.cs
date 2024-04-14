using System;
using System.Collections.Generic;

namespace pbERP.Domain.Models.AGeneralConfig;

public partial class AGenConfigMGlobalOption
{
    public long GlobalOptionId { get; set; }

    public string GlobalOptionDescription { get; set; }

    public string GlobalOptionValue { get; set; }

    public string GlobalOptionDefaultValue { get; set; }

    public string GlobalOptionValueDefinition { get; set; }

    public string BoothId { get; set; }

    public string BranchId { get; set; }
}

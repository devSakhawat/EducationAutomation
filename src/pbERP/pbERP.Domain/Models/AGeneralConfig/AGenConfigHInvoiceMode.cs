using System;
using System.Collections.Generic;

namespace pbERP.Domain.Models.AGeneralConfig;

public partial class AGenConfigHInvoiceMode
{
    public long InvoiceModeId { get; set; }

    public string InvoiceModeName { get; set; }

    public virtual ICollection<AGenConfigKBooth> AGenConfigKBooths { get; set; } = new List<AGenConfigKBooth>();
}

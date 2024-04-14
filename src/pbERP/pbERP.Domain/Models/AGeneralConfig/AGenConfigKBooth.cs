using System;
using System.Collections.Generic;

namespace pbERP.Domain.Models.AGeneralConfig;

public partial class AGenConfigKBooth
{
    public long BoothId { get; set; }

    public string BoothCode { get; set; }

    public string BoothName { get; set; }

    public string ComputerName { get; set; }

    public long? LanguageId { get; set; }

    public long? FontId { get; set; }

    public long? InvoiceModeId { get; set; }

    public virtual AGenConfigGFont Font { get; set; }

    public virtual AGenConfigHInvoiceMode InvoiceMode { get; set; }

    public virtual AGenConfigFLanguage Language { get; set; }
}

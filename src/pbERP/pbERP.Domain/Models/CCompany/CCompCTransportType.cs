using System;
using System.Collections.Generic;

namespace pbERP.Domain.Models.CCompany;

public partial class CCompCTransportType
{
    public long TransportTypeId { get; set; }

    public string TransportType { get; set; }

    public string TransportTypeLocal { get; set; }

    public long? CompanyId { get; set; }

    public virtual ICollection<CCompDTransport> CCompDTransports { get; set; } = new List<CCompDTransport>();

    public virtual CCompACompany Company { get; set; }
}

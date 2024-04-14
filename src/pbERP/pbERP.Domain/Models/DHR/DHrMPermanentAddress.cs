using System;
using System.Collections.Generic;
using pbERP.Domain.Models.AGeneralConfig;

namespace pbERP.Domain.Models.DHR;

public partial class DHrMPermanentAddress
{
    public long PermanentAddressId { get; set; }

    public string PermanentAddress { get; set; }

    public string PermanentAddressLocal { get; set; }

    public string PostOffice { get; set; }

    public string PostOfficeLocal { get; set; }

    public long? PoliceStationId { get; set; }

    public long? ReferenceTypeId { get; set; }

    public long? ReferenceId { get; set; }

    public virtual AGenConfigDPoliceStation PoliceStation { get; set; }

    public virtual DHrKReferenceType ReferenceType { get; set; }
}

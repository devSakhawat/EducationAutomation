using System;
using System.Collections.Generic;
using pbERP.Domain.Models.CCompany;
using pbERP.Domain.Models.DHR;

namespace pbERP.Domain.Models.AGeneralConfig;

public partial class AGenConfigDPoliceStation
{
    public long PoliceStationId { get; set; }

    public string PoliceStationName { get; set; }

    public string PoliceStationNameLocal { get; set; }

    public string PostalCode { get; set; }

    public long? DistrictId { get; set; }

    public virtual ICollection<CCompACompany> CCompACompanies { get; set; } = new List<CCompACompany>();

    public virtual ICollection<DHrJEmployee> DHrJEmployees { get; set; } = new List<DHrJEmployee>();

    public virtual ICollection<DHrLPresentAddress> DHrLPresentAddresses { get; set; } = new List<DHrLPresentAddress>();

    public virtual ICollection<DHrMPermanentAddress> DHrMPermanentAddresses { get; set; } = new List<DHrMPermanentAddress>();

    public virtual AGenConfigCDistrictOrCity District { get; set; }
}

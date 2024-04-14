using System;
using System.Collections.Generic;
using pbERP.Domain.Models.CCompany;

namespace pbERP.Domain.Models.AGeneralConfig;

public partial class AGenConfigEBusinessType
{
    public long BusinessTypeId { get; set; }

    public string BusinessTypeName { get; set; }

    public string BusinessTypeNameLocal { get; set; }

    public virtual ICollection<CCompACompany> CCompACompanies { get; set; } = new List<CCompACompany>();
}

using System;
using System.Collections.Generic;

namespace pbERP.Domain.Models.DHR;

public partial class DHrKReferenceType
{
    public long ReferenceTypeId { get; set; }

    public string ReferenceTypeName { get; set; }

    public string ReferenceTypeNameLocal { get; set; }

    public virtual ICollection<DHrLPresentAddress> DHrLPresentAddresses { get; set; } = new List<DHrLPresentAddress>();

    public virtual ICollection<DHrMPermanentAddress> DHrMPermanentAddresses { get; set; } = new List<DHrMPermanentAddress>();
}

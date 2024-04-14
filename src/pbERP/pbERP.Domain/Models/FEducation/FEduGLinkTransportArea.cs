using System;
using System.Collections.Generic;
using pbERP.Domain.Models.CCompany;

namespace pbERP.Domain.Models.FEducation;

public partial class FEduGLinkTransportArea
{
    public long LinkAreaTransportId { get; set; }

    public long? TransportAreaId { get; set; }

    public long? TransportId { get; set; }

    public virtual CCompDTransport Transport { get; set; }

    public virtual FEduETransportArea TransportArea { get; set; }
}

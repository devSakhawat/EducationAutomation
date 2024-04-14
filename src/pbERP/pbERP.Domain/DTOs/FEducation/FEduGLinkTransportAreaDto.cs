namespace pbERP.Domain.DTOs.FEducation;

public class FEduGLinkTransportAreaDto
{
   public long LinkAreaTransportId { get; set; }

   public long? TransportAreaId { get; set; }

   public long? TransportId { get; set; }

   public string TransportName { get; set; }

   public string TransportAreaName { get; set; }

   //public virtual CCompDTransport Transport { get; set; }

   //public virtual FEduETransportArea TransportArea { get; set; }
}

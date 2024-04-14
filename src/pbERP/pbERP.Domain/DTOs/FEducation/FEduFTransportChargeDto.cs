namespace pbERP.Domain.DTOs.FEducation;

public class FEduFTransportChargeDto
{
   public long TransportChargeId { get; set; }

   public long? TransportAreaId { get; set; }

   public long? TransportId { get; set; }

   public double? TransportCharge { get; set; }


   public string? TransportName { get; set; }

   public string? TransportAreaName { get; set; }

   //public virtual CCompDTransport Transport { get; set; }

   //public virtual FEduETransportArea TransportArea { get; set; }
}

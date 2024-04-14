namespace pbERP.Domain.DTOs.FEducation;

public class FEduETransportAreaDto
{
   public long TransportAreaId { get; set; }

   public string TransportAreaName { get; set; }

   public double? AreaDistance { get; set; }

   //public virtual ICollection<FEduFTransportCharge> FEduFTransportCharges { get; set; } = new List<FEduFTransportCharge>();

   //public virtual ICollection<FEduGLinkTransportArea> FEduGLinkTransportAreas { get; set; } = new List<FEduGLinkTransportArea>();

   //public virtual ICollection<FEduHStudentAllocateTransport> FEduHStudentAllocateTransports { get; set; } = new List<FEduHStudentAllocateTransport>();
}

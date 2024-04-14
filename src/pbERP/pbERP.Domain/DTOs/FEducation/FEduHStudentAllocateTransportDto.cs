namespace pbERP.Domain.DTOs.FEducation;

public class FEduHStudentAllocateTransportDto
{
   public long AllocateTransportId { get; set; }

   public long? StudentId { get; set; }

   public long? TransportId { get; set; }

   public long? TransportAreaId { get; set; }

   public string StudentName { get; set; }

   public string TransportName { get; set; }

   public string TransportAreaName { get; set; }

   //public virtual FEduAStudent Student { get; set; }

   //public virtual CCompDTransport Transport { get; set; }

   //public virtual FEduETransportArea TransportArea { get; set; }
}

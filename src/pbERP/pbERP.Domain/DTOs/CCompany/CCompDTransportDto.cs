using pbERP.Domain.Models.CCompany;
using pbERP.Domain.Models.FEducation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pbERP.Domain.DTOs.CCompany;

public class CCompDTransportDto
{
   public long TransportId { get; set; }

   public string TransportName { get; set; }

   public string TransportNameLocal { get; set; }

   public long? TransportTypeId { get; set; }

   public string TransportTypeName { get; set; }

   public int? TransportSeatCapacity { get; set; }

   public string TransportEngineNo { get; set; }

   public string TransportRegNo { get; set; }

   public double? EnginePower { get; set; }

   public double? TaxToken { get; set; }

   public double? Fitness { get; set; }

   public double? Ait { get; set; }

   public DateTime? FitnessExpiry { get; set; }

   public DateTime? TaxTokenExpiry { get; set; }

   //public virtual CCompCTransportType TransportType { get; set; }

   //public virtual ICollection<FEduFTransportCharge> FEduFTransportCharges { get; set; } = new List<FEduFTransportCharge>();

   //public virtual ICollection<FEduGLinkTransportArea> FEduGLinkTransportAreas { get; set; } = new List<FEduGLinkTransportArea>();

   //public virtual ICollection<FEduHStudentAllocateTransport> FEduHStudentAllocateTransports { get; set; } = new List<FEduHStudentAllocateTransport>();

}

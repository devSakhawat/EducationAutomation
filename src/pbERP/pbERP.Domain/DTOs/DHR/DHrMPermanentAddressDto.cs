namespace pbERP.Domain.DTOs.DHR;

public class DHrMPermanentAddressDto
{
   public long PermanentAddressId { get; set; }

   public string PermanentAddress { get; set; }

   public string PermanentAddressLocal { get; set; }

   public string PostOffice { get; set; }

   public string PostOfficeLocal { get; set; }

   public long? PoliceStationId { get; set; }

   public long? ReferenceTypeId { get; set; }

   public long? ReferenceId { get; set; }

   public string PoliceStationName { get; set; }

   public string ReferenceTypeName { get; set; }

   //   public virtual AGenConfigDPoliceStation PoliceStation { get; set; }

   //   public virtual DHrKReferenceType ReferenceType { get; set; }
}
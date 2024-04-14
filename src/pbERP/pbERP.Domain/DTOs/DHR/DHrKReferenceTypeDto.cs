namespace pbERP.Domain.DTOs.DHR;

public class DHrKReferenceTypeDto
{
   public long ReferenceTypeId { get; set; }

   public string ReferenceTypeName { get; set; }

   public string ReferenceTypeNameLocal { get; set; }

   public string PresentAddress { get; set; }

   public string PermanentAddresses { get; set; }

   //public virtual ICollection<DHrLPresentAddress> DHrLPresentAddresses { get; set; } = new List<DHrLPresentAddress>();

   //public virtual ICollection<DHrMPermanentAddress> DHrMPermanentAddresses { get; set; } = new List<DHrMPermanentAddress>();
}
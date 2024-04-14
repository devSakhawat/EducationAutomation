using Microsoft.AspNetCore.Http;

namespace pbERP.Domain.DTOs.CCompany;

public class CCompACompanyDto
{
   public long CompanyId { get; set; }

   public string GroupOfCompanyName { get; set; }

   public string CompanyCode { get; set; }

   public string CompanyName { get; set; }

   public string CompanyAddress { get; set; }

   public long? PoliceStationId { get; set; }
   public string? PoliceStationName { get; set; }

   public string CompanyPhone { get; set; }

   public string CompanyFax { get; set; }

   public string CompanyWhatsApp { get; set; }

   public string CompanyEmailAddress { get; set; }

   public string CompanyWebAddress { get; set; }

   public string CompanyTin { get; set; }

   public string CompanyBin { get; set; }

   public string CompanyEin { get; set; }

   public string CompanyVatRegistration { get; set; }

   public long? BusinessTypeId { get; set; }
   public string? BusinessTypeName { get; set; }

   public byte[] CompanyLogo { get; set; }
   public IFormFile? CompanyLogoImage { get; set; }

   public byte[] CompanyBackgroundImage { get; set; }
   public IFormFile? CompanyBackgroundImageIFormFIle { get; set; }

   //public virtual AGenConfigDPoliceStation PoliceStation { get; set; }
   //public virtual AGenConfigEBusinessType BusinessType { get; set; }

   //public virtual ICollection<AGenConfigJCompanyLinkModule> AGenConfigJCompanyLinkModules { get; set; } = new List<AGenConfigJCompanyLinkModule>();

   //public virtual ICollection<CCompBBranch> CCompBBranches { get; set; } = new List<CCompBBranch>();

   //public virtual ICollection<CCompCTransportType> CCompCTransportTypes { get; set; } = new List<CCompCTransportType>();

   //public virtual ICollection<FEduBBuilding> FEduBBuildings { get; set; } = new List<FEduBBuilding>();
}

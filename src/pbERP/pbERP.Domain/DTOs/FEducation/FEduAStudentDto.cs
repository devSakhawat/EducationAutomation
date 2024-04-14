using Microsoft.AspNetCore.Http;

namespace pbERP.Domain.DTOs.FEducation;

public class FEduAStudentDto
{
   public long StudentId { get; set; }
   public string StudentCode { get; set; }
   public string StudentName { get; set; }
   public string? StudentFathersName { get; set; }
   public string? StudentMothersName { get; set; }
   public long? GenderId { get; set; }
   public long? BloodGroupId { get; set; }
   public long? ReligionId { get; set; }
   public byte[]? StudentPhoto { get; set; }
   public IFormFile? StudentImage { get; set; }

   public string? GenderName { get; set; }
   public string? BloodGroupName { get; set; }
   public string? ReligionName { get; set; }

   // Present Address
   public long? PresentAddressId { get; set; } = null;
   public string? PresentAddress { get; set; }
   public string? PresentPostOffice { get; set; }
   public long? PresentPoliceStationId { get; set; }
   //public long PresentReferenceTypeId { get; set; }
   //public long PresentReferenceId { get; set; }
   public string? PresentPoliceStationName { get; set; }
   public long? PresentDistrictId { get; set; }
   public string? PresentDistrictName { get; set; }
   public long? PresentDivisionId { get; set; }
   public string? PresentDivisionName { get; set; }
   public long? PresentCountryId { get; set; }
   public string? PresentCountryName { get; set; }
   //public string? PresentReferenceTypeName { get; set; }

   // Premanent Address
   public long? PermanentAddressId { get; set; } = null;
   public string? PermanentAddress { get; set; }
   public string? PermanentPostOffice { get; set; }
   public long? PermanentPoliceStationId { get; set; }
   //public long PermanentReferenceTypeId { get; set; }
   //public long PermanentReferenceId { get; set; }

   public string? PermanentPoliceStationName { get; set; }
   public long? PermanentDistrictId { get; set; }
   public string? PermanentDistrictName { get; set; }
   public long? PermanentDivisionId { get; set; }
   public string? PermanentDivisionName { get; set; }
   public long? PermanentCountryId { get; set; }
   public string? PermanentCountryName { get; set; }
   //public string? PermanentReferenceTypeName { get; set; }
}



namespace pbERP.Domain.DTOs.Education;

public class EduBBuildingDto
{
   public long BuildingId { get; set; }

   public string? BuildingNameEnglish { get; set; }

   public string? BuildingNameLocal { get; set; }

   public string? UsesType { get; set; }

   public long? CompanyId { get; set; }

   public string? CompanyName { get; set; }
}

public class EduBBuildingToReturnDto
{
   public long BuildingId { get; set; }

   public string? BuildingNameEnglish { get; set; }

   public string? BuildingNameLocal { get; set; }

   public string? UsesType { get; set; }

   public string? CompanyName { get; set; }
}

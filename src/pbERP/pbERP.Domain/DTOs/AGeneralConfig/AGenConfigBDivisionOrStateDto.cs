namespace pbERP.Domain.DTOs.AGeneralConfig;

public class AGenConfigBDivisionOrStateDto
{
   public long DivisionId { get; set; }

   public string DivisionName { get; set; }

   public long? CountryId { get; set; }

   public string? CountryName { get; set; }
}

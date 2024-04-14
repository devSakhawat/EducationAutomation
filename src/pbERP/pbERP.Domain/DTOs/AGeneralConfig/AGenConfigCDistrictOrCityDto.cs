namespace pbERP.Domain.DTOs.AGeneralConfig;

public class AGenConfigCDistrictOrCityDto
{
   public long DistrictId { get; set; }

   public string DistrictName { get; set; }

   public long? DivisionId { get; set; }

   public string? DivisionName { get; set; }
}

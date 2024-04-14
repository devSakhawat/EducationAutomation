namespace pbERP.Domain.DTOs.AGeneralConfig;

public class AGenConfigDPoliceStationDto
{
   public long PoliceStationId { get; set; }

   public string PoliceStationName{ get; set; }

   public string PostalCode { get; set; }

   public long? DistrictId { get; set; }

   public string? DistrictName { get; set; }
}

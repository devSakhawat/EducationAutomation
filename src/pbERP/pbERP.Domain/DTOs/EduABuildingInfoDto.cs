namespace pbERP.Domain.DTOs;

public record EduABuildingInfoDto
(
    decimal BuildingId,

    string? BuildingNameEnglish,

    string? BuildingNameLocal,

    string? UsesType,

    decimal? CompanyId

//virtual CompACompanyInfo? Company,

//virtual ICollection<EduBClassOrHallRoomInfo> EduBClassOrHallRoomInfos, = new List<EduBClassOrHallRoomInfo>();
);



public class EduABuildingInfoToReturnDto
{
   public int BuildingId { get; set; }

   public string? BuildingNameEnglish { get; set; }

   public string? BuildingNameLocal { get; set; }

   public string? UsesType { get; set; }

   public string? Company { get; set; }
}

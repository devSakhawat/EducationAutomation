namespace pbERP.Domain.DTOs.FEducation;

public class FEduBBuildingDto
{
  public long BuildingId { get; set; }

  public string BuildingName { get; set; }

  public string UsesType { get; set; }

  public long? CompanyId { get; set; }

  public string? CompanyName { get; set; }
}

// Building can be save with Building Name only.

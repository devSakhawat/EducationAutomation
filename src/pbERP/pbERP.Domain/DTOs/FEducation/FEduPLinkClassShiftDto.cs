using pbERP.Domain.Models.FEducation;

namespace pbERP.Domain.DTOs.FEducation;

public class FEduPLinkClassShiftDto
{
  public long LinkClassShiftId { get; set; }

  public long? ClassId { get; set; }

  public string? ClassName { get; set; }

  public long? ClassShiftId { get; set; }

  public string? ClassShiftName { get; set; }
}

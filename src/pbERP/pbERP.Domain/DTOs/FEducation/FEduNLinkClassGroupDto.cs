using pbERP.Domain.Models.FEducation;

namespace pbERP.Domain.DTOs.FEducation;

public class FEduNLinkClassGroupDto
{
  public long LinkClassGroupId { get; set; }

  public long? ClassId { get; set; }

  public long? ClassGroupId { get; set; }

  public string? ClassName { get; set; }

  public string? ClassGroupName { get; set; }

  //public virtual FEduAClass Class { get; set; }

  //public virtual FEduKClassGroup ClassGroup { get; set; }
}


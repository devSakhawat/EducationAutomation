using pbERP.Domain.Models.FEducation;

namespace pbERP.Domain.DTOs.FEducation;

public class FEduQLinkClassSubjectDto
{
  public long LinkClassSubjectId { get; set; }

  public long? ClassId { get; set; }

  public string? ClassName { get; set; }

  public long? ClassGroupId { get; set; }

  public string? ClassGroupName { get; set; }

  public long? ClassSubjectId { get; set; }

  public string? ClassSubjectName { get; set; }

  //public virtual FEduAClass Class { get; set; }

  //public virtual FEduKClassGroup ClassGroup { get; set; }

  //public virtual FEduMClassSubject ClassSubject { get; set; }
}

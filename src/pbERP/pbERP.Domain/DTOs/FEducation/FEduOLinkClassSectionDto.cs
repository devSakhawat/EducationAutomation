using pbERP.Domain.Models.FEducation;

namespace pbERP.Domain.DTOs.FEducation;

public class FEduOLinkClassSectionDto
{
  public long LinkClassSectionId { get; set; }

  public long? ClassId { get; set; }

  public long? ClassSectionId { get; set; }

  public string ClassName { get; set; }

  public string ClassSectionName { get; set; }

  //public virtual FEduAClass Class { get; set; }

  //public virtual FEduJClassSection ClassSection { get; set; }
}

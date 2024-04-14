using pbERP.Domain.Models.FEducation;

namespace pbERP.Infrastructure.Specifications.Education;

public class FEduOLinkClassSectionSpecification : BaseSpecification<FEduOLinkClassSection>
{
  public FEduOLinkClassSectionSpecification(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) || x.LinkClassSectionId.ToString().ToLower().Contains(specParams.Search))
  {
    AddInclude(x => x.Class);
    AddInclude(x => x.ClassSection);
    if (!string.IsNullOrEmpty(specParams.Sort))
    {
      switch (specParams.Sort)
      {
        case "nameAsc": AddOrderBy(s => s.ClassId); break;
        case "nameDesc": AddOrderByDescending(s => s.ClassId); break;
        default: AddOrderBy(s => s.LinkClassSectionId); break;
      }
    }
  }

  public FEduOLinkClassSectionSpecification(long id) : base(e => e.LinkClassSectionId == id)
  {
    AddInclude(x => x.Class);
    AddInclude(x => x.ClassSection);
  }
}

public class FEduOLinkClassSectionCount : BaseSpecification<FEduOLinkClassSection>
{
  public FEduOLinkClassSectionCount(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) || x.ClassId.ToString().ToLower().Contains(specParams.Search))
  {
  }
}

public class FEduOLinkClassSectionDelete : BaseSpecification<FEduOLinkClassSection>
{
  public FEduOLinkClassSectionDelete(long id) : base(x => x.LinkClassSectionId == id)
  {
  }
}
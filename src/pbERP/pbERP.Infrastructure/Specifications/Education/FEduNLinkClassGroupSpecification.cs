using pbERP.Domain.Models.FEducation;

namespace pbERP.Infrastructure.Specifications.Education;

public class FEduNLinkClassGroupSpecification : BaseSpecification<FEduNLinkClassGroup>
{
  public FEduNLinkClassGroupSpecification(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) || x.LinkClassGroupId.ToString().ToLower().Contains(specParams.Search))
  {
    AddInclude(x => x.Class);
    AddInclude(x => x.ClassGroup);
    if (!string.IsNullOrEmpty(specParams.Sort))
    {
      switch (specParams.Sort)
      {
        case "nameAsc": AddOrderBy(s => s.ClassId); break;
        case "nameDesc": AddOrderByDescending(s => s.ClassId); break;
        default: AddOrderBy(s => s.LinkClassGroupId); break;
      }
    }
  }
  public FEduNLinkClassGroupSpecification(long id) : base(e => e.LinkClassGroupId == id)
  {
    AddInclude(x => x.Class);
    AddInclude(x => x.ClassGroup);
  }
}

public class FEduNLinkClassGroupCount : BaseSpecification<FEduNLinkClassGroup>
{
  public FEduNLinkClassGroupCount(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) || x.ClassId.ToString().ToLower().Contains(specParams.Search))
  {
  }
}

public class FEduNLinkClassGroupDelete : BaseSpecification<FEduNLinkClassGroup>
{
  public FEduNLinkClassGroupDelete(long id) : base(x => x.LinkClassGroupId == id)
  {
  }
}

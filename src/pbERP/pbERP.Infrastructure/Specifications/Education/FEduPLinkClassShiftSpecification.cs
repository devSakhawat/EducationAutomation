using pbERP.Domain.Models.FEducation;

namespace pbERP.Infrastructure.Specifications.Education;

public class FEduPLinkClassShiftSpecification : BaseSpecification<FEduPLinkClassShift>
{
  public FEduPLinkClassShiftSpecification(SpecificationParams specParams)
    : base(x => string.IsNullOrEmpty(specParams.Search) || x.LinkClassShiftId.ToString().ToLower().Contains(specParams.Search))
  {
    AddInclude(x => x.Class);
    AddInclude(x => x.ClassShift);

    if (!string.IsNullOrEmpty(specParams.Sort))
    {
      switch (specParams.Sort)
      {
        case "nameAsc": AddOrderBy(x => x.ClassId); break;
        case "nameDesc": AddOrderByDescending(x => x.ClassId); break;
        default: AddOrderBy(x => x.LinkClassShiftId); break;
      }
    }
  }

  public FEduPLinkClassShiftSpecification(long id) : base(x => x.LinkClassShiftId == id)
  {
    AddInclude(x => x.Class);
    AddInclude(x => x.ClassShift);
  }
}


public class FEduPLinkClassShiftCount : BaseSpecification<FEduPLinkClassShift>
{
  public FEduPLinkClassShiftCount(SpecificationParams specParams)
    : base(x => string.IsNullOrEmpty(specParams.Search) || x.ClassId.ToString().ToLower().Contains(specParams.Search))
  {

  }
}

public class FEduPLinkClassShiftDelete : BaseSpecification<FEduPLinkClassShift>
{
  public FEduPLinkClassShiftDelete(long id) : base(x => x.LinkClassShiftId == id)
  {
  }
}

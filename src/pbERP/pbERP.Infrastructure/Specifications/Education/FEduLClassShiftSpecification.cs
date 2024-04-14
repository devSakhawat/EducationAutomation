using pbERP.Domain.Models.FEducation;

namespace pbERP.Infrastructure.Specifications.Education;

public class FEduLClassShiftSpecification : BaseSpecification<FEduLClassShift>
{
   public FEduLClassShiftSpecification(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) || x.ClassShiftName.ToLower().Contains(specParams.Search))
   {
      AddOrderByDescending(s => s.ClassShiftId);
      ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

      if (!string.IsNullOrEmpty(specParams.Sort))
      {
         switch (specParams.Sort)
         {
            case "nameAsc": AddOrderBy(x => x.ClassShiftName); break;
            case "nameDesc": AddOrderByDescending(x => x.ClassShiftName); break;
            default: AddOrderByDescending(x => x.ClassShiftId); break;
         }
      }
   }
   public FEduLClassShiftSpecification(long id) : base(e => e.ClassShiftId == id)
   {
   }
}

public class FEduLClassShiftCount : BaseSpecification<FEduLClassShift>
{
   public FEduLClassShiftCount(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) || x.ClassShiftName.ToLower().Contains(specParams.Search))
   {
   }
}

public class FEduLClassShiftDelete : BaseSpecification<FEduLClassShift>
{
   public FEduLClassShiftDelete(long id) : base(x => x.ClassShiftId == id)
   {
      AddInclude(x => x.FEduPLinkClassShifts);
   }
}

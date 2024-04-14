using pbERP.Domain.Models.FEducation;

namespace pbERP.Infrastructure.Specifications.Education;

public class FEduUClassPeriodSpecification : BaseSpecification<FEduUClassPeriod>
{
   public FEduUClassPeriodSpecification(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) || x.ClassPeriodName.ToLower().Contains(specParams.Search))
   {
      AddOrderByDescending(s => s.ClassPeriodId);
      ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

      if (!string.IsNullOrEmpty(specParams.Sort))
      {
         switch (specParams.Sort)
         {
            case "nameAsc": AddOrderBy(x => x.ClassPeriodName); break;
            case "nameDesc": AddOrderByDescending(x => x.ClassPeriodName); break;
            default: AddOrderByDescending(x => x.ClassPeriodId); break;
         }
      }
   }
   public FEduUClassPeriodSpecification(long id) : base(e => e.ClassPeriodId == id)
   {
   }
}

public class FEduUClassPeriodCount : BaseSpecification<FEduUClassPeriod>
{
   public FEduUClassPeriodCount(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) || x.ClassPeriodName.ToLower().Contains(specParams.Search))
   {
   }
}

public class FEduUClassPeriodDelete : BaseSpecification<FEduUClassPeriod>
{
   public FEduUClassPeriodDelete(long id) : base(x => x.ClassPeriodId == id)
   {
   }
}

using pbERP.Domain.Models.AGeneralConfig;

namespace pbERP.Infrastructure.Specifications.GeneralConfig;

public class AGenConfigGReligionSpecification : BaseSpecification<AGenConfigGReligion>
{
    public AGenConfigGReligionSpecification(SpecificationParams speckParams) : base (x => string.IsNullOrEmpty(speckParams.Search) || x.ReligionName.ToLower().Contains(speckParams.Search))
    {
      AddOrderByDescending(x => x.ReligionId);
      ApplyPaging(speckParams.PageSize * (speckParams.PageIndex - 1), speckParams.PageSize);

      if (!string.IsNullOrEmpty(speckParams.Sort))
      {
         switch(speckParams.Sort)
         {
            case "nameAsc": AddOrderBy(x => x.ReligionName); break;
            case "nameDesc": AddOrderByDescending(x => x.ReligionName); break;
            default: AddOrderByDescending(x => x.ReligionId ); break;
         }
      }
    }

   public AGenConfigGReligionSpecification(long id) : base(x => x.ReligionId == id )
   {
   }
}

public class AGenConfigGReligionCount : BaseSpecification<AGenConfigGReligion>
{
    public AGenConfigGReligionCount(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) || x.ReligionName.ToLower().Contains(specParams.Search))
    {
    }
}

public class AGenConfigGReligionDelete : BaseSpecification<AGenConfigGReligion>
{
   public AGenConfigGReligionDelete(long id) : base(u => u.ReligionId == id)
   {
      AddInclude(x => x.DHrJEmployees);
      AddInclude(x => x.FEduAStudents);
   }
}
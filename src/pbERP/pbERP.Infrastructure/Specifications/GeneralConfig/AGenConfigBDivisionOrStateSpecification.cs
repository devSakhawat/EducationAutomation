using pbERP.Domain.Models.AGeneralConfig;

namespace pbERP.Infrastructure.Specifications.GeneralConfig;

public class AGenConfigBDivisionOrStateSpecification : BaseSpecification<AGenConfigBDivisionOrState>
{
    public AGenConfigBDivisionOrStateSpecification(SpecificationParams speckParams) : base (x => string.IsNullOrEmpty(speckParams.Search) || x.DivisionName.ToLower().Contains(speckParams.Search))
    {
      AddInclude(x => x.Country);
      AddOrderByDescending(x => x.DivisionId);
      ApplyPaging(speckParams.PageSize * (speckParams.PageIndex - 1), speckParams.PageSize);

      if (!string.IsNullOrEmpty(speckParams.Sort))
      {
         switch(speckParams.Sort)
         {
            case "nameAsc": AddOrderBy(x => x.DivisionName); break;
            case "nameDesc": AddOrderByDescending(x => x.DivisionName); break;
            default: AddOrderByDescending(x => x.DivisionId); break;
         }
      }
    }

   public AGenConfigBDivisionOrStateSpecification(long id) : base(x => x.DivisionId == id )
   {
      AddInclude(x => x.Country);
   }
}

public class AGenConfigBDivisionOrStateCount : BaseSpecification<AGenConfigBDivisionOrState>
{
    public AGenConfigBDivisionOrStateCount(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) || x.DivisionName.ToLower().Contains(specParams.Search))
    {
      AddInclude(x => x.Country);
    }
}

public class AGenConfigBDivisionOrStateDelete : BaseSpecification<AGenConfigBDivisionOrState>
{
   public AGenConfigBDivisionOrStateDelete(long id) : base(u => u.DivisionId == id)
   {
      AddInclude(x => x.AGenConfigCDistrictOrCities);
   }
}
using pbERP.Domain.Models.AGeneralConfig;

namespace pbERP.Infrastructure.Specifications.GeneralConfig;

public class AGenConfigCDistrictOrCitySpecification : BaseSpecification<AGenConfigCDistrictOrCity>
{
   public AGenConfigCDistrictOrCitySpecification(SpecificationParams speckParams) : base(x => string.IsNullOrEmpty(speckParams.Search) || x.DistrictName.ToLower().Contains(speckParams.Search))
   {
      AddInclude(x => x.Division);
      AddOrderByDescending(x => x.DistrictId);
      ApplyPaging(speckParams.PageSize * (speckParams.PageIndex - 1), speckParams.PageSize);

      if (!string.IsNullOrEmpty(speckParams.Sort))
      {
         switch (speckParams.Sort)
         {
            case "nameAsc": AddOrderBy(x => x.DistrictName); break;
            case "nameDesc": AddOrderByDescending(x => x.DistrictName); break;
            default: AddOrderByDescending(x => x.DistrictId); break;
         }
      }
   }

   public AGenConfigCDistrictOrCitySpecification(long id) : base(x => x.DistrictId == id)
   {
      AddInclude(x => x.Division);
   }
}

public class AGenConfigCDistrictOrCityCount : BaseSpecification<AGenConfigCDistrictOrCity>
{
   public AGenConfigCDistrictOrCityCount(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) || x.DistrictName.ToLower().Contains(specParams.Search))
   {
   }
}

public class AGenConfigCDistrictOrCityDelete : BaseSpecification<AGenConfigCDistrictOrCity>
{
   public AGenConfigCDistrictOrCityDelete(long id) : base(u => u.DistrictId == id)
   {
      AddInclude(x => x.AGenConfigDPoliceStations);
   }
}


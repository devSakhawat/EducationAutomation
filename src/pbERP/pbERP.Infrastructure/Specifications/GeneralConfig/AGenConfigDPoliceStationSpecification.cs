using pbERP.Domain.Models.AGeneralConfig;

namespace pbERP.Infrastructure.Specifications.GeneralConfig;

public class AGenConfigDPoliceStationSpecification : BaseSpecification<AGenConfigDPoliceStation>
{
   public AGenConfigDPoliceStationSpecification(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) || x.PoliceStationName.ToLower().Contains(specParams.Search))
   {
      AddInclude(x => x.District);
      AddOrderByDescending(x => x.PoliceStationId);
      ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

      if (!string.IsNullOrEmpty(specParams.Sort))
      {
         switch (specParams.Sort)
         {
            case "nameAsc": AddOrderBy(x => x.PoliceStationName); break;
            case "nameDesc": AddOrderByDescending(x => x.PoliceStationName); break;
            default: AddOrderByDescending(x => x.PoliceStationId); break;
         }
      }
   }

   public AGenConfigDPoliceStationSpecification(long id) : base(x => x.PoliceStationId == id)
   {
      AddInclude(x => x.District);
   }
}

public class AGenConfigDPoliceStationCount : BaseSpecification<AGenConfigDPoliceStation>
{
   public AGenConfigDPoliceStationCount(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) || x.PoliceStationName.ToLower().Contains(specParams.Search))
   {

   }
}

public class AGenConfigDPoliceStationDelete : BaseSpecification<AGenConfigDPoliceStation>
{
   public AGenConfigDPoliceStationDelete(long id) : base(x => x.PoliceStationId == id)
   {
      AddInclude(x => x.CCompACompanies);
      AddInclude(x => x.DHrJEmployees);
      AddInclude(x => x.DHrLPresentAddresses);
      AddInclude(x => x.DHrMPermanentAddresses);
   }
}

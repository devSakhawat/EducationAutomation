using pbERP.Domain.Models.AGeneralConfig;

namespace pbERP.Infrastructure.Specifications.GeneralConfig;

public class AGenConfigACountrySpecification : BaseSpecification<AGenConfigACountry>
{
    public AGenConfigACountrySpecification(SpecificationParams speckParams) : base (x => string.IsNullOrEmpty(speckParams.Search) || x.CountryName.ToLower().Contains(speckParams.Search))
    {
      AddOrderByDescending(x => x.CountryId);
      ApplyPaging(speckParams.PageSize * (speckParams.PageIndex - 1), speckParams.PageSize);

      if (!string.IsNullOrEmpty(speckParams.Sort))
      {
         switch(speckParams.Sort)
         {
            case "nameAsc": AddOrderBy(x => x.CountryName); break;
            case "nameDesc": AddOrderByDescending(x => x.CountryName); break;
            default: AddOrderByDescending(x => x.CountryId); break;
         }
      }
    }

   public AGenConfigACountrySpecification(long id) : base(x => x.CountryId == id )
   {
   }
}

public class AGenConfigACountryCount : BaseSpecification<AGenConfigACountry>
{
    public AGenConfigACountryCount(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) || x.CountryName.ToLower().Contains(specParams.Search))
    {
    }
}

public class AGenConfigACountryDelete : BaseSpecification<AGenConfigACountry>
{
   public AGenConfigACountryDelete(long id) : base(u => u.CountryId == id)
   {
      AddInclude(x => x.AGenConfigBDivisionOrStates);
   }
}
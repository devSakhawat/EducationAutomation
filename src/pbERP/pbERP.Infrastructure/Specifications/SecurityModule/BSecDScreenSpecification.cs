using pbERP.Domain.Models.BSecurity;

namespace pbERP.Infrastructure.Specifications.SecurityModule;

public class BSecDScreenSpecification : BaseSpecification<BSecDScreen>
{
   public BSecDScreenSpecification(SpecificationParams specParams) : base(x =>
   string.IsNullOrEmpty(specParams.Search) || x.ScreenName.ToLower().Contains(specParams.Search))
   {
      AddInclude(x => x.Module);
      AddOrderByDescending(u => u.ScreenId);
      ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

      if (!string.IsNullOrEmpty(specParams.Sort))
      {
         switch (specParams.Sort)
         {
            case "nameAsc": AddOrderBy(u => u.ScreenName); break;
            case "nameDesc": AddOrderByDescending(u => u.ScreenName); break;
            default: AddOrderBy(u => u.ScreenName); break;
         }
      }
   }

   public BSecDScreenSpecification(long id) : base(u => u.ScreenId == id)
   {
      AddInclude(x => x.Module);
   }
}

public class BSecDScreenCount : BaseSpecification<BSecDScreen>
{
   public BSecDScreenCount(SpecificationParams specParams) : base(x =>
   string.IsNullOrEmpty(specParams.Search) || x.ScreenName.ToLower().Contains(specParams.Search))
   {
   }
}

public class BSecDScreenDelete : BaseSpecification<BSecDScreen>
{
   public BSecDScreenDelete(long id) : base(u => u.ScreenId == id)
   {
      AddInclude(x => x.BSecELinkUserGroupScreens);
      AddInclude(x => x.BSecFScreenCommands);
   }
}

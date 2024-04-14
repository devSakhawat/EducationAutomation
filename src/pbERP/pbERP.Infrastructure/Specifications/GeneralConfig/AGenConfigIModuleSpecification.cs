using pbERP.Domain.Models.AGeneralConfig;

namespace pbERP.Infrastructure.Specifications.GeneralConfig;

public class AGenConfigIModuleSpecification : BaseSpecification<AGenConfigIModule>
{
   public AGenConfigIModuleSpecification(SpecificationParams specParams) : base(x =>
   string.IsNullOrEmpty(specParams.Search) || x.ModuleName.ToLower().Contains(specParams.Search))
   {
      AddOrderByDescending(u => u.ModuleId);
      ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

      if (!string.IsNullOrEmpty(specParams.Sort))
      {
         switch (specParams.Sort)
         {
            case "nameAsc": AddOrderBy(u => u.ModuleName); break;
            case "nameDesc": AddOrderByDescending(u => u.ModuleName); break;
            default: AddOrderBy(u => u.ModuleName); break;
         }
      }
   }

   public AGenConfigIModuleSpecification(long id) : base(u => u.ModuleId == id)
   {
   }
}

public class AGenConfigIModuleCount : BaseSpecification<AGenConfigIModule>
{
   public AGenConfigIModuleCount(SpecificationParams specParams) : base(x =>
   string.IsNullOrEmpty(specParams.Search) || x.ModuleName.ToLower().Contains(specParams.Search))
   {
   }
}

public class AGenConfigIModuleDelete : BaseSpecification<AGenConfigIModule>
{
   public AGenConfigIModuleDelete(long id) : base(u => u.ModuleId == id)
   {
      AddInclude(x => x.AGenConfigJCompanyLinkModules);
      AddInclude(x => x.BSecDScreens);
   }
}

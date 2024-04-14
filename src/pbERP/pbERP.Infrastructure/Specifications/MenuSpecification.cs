using pbERP.Domain.Models;
using pbERP.Domain.Models.AGeneralConfig;

namespace pbERP.Infrastructure.Specifications;

// Specification to get main menu
public class MainMenuSpecification : BaseSpecification<AGenConfigJCompanyLinkModule>
{
   public MainMenuSpecification(long companyId) : base(e => e.CompanyId == companyId)
   {
      AddInclude(e => e.Module);
   }
}

// Specification to get sub menu
//public class SubMenuSpecification : BaseSpecification<SecDScreen>
//{
//   public SubMenuSpecification(long moduleId) : base(e => e.ModuleId == moduleId)
//   {
//      AddInclude(e => e.Module);
//   }
//}
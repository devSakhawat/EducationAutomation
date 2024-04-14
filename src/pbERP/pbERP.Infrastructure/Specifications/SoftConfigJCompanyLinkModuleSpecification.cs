using pbERP.Domain.Models;
using pbERP.Domain.Models.AGeneralConfig;

namespace pbERP.Infrastructure.Specifications
{
   public class SoftConfigJCompanyLinkModuleSpecification : BaseSpecification<AGenConfigJCompanyLinkModule>
   {
      public SoftConfigJCompanyLinkModuleSpecification(SpecificationParams specParams) : base(x =>
          (string.IsNullOrEmpty(specParams.Search) || x.Company.CompanyName.ToLower().Contains(specParams.Search)) &&
          (!specParams.Id.HasValue || x.CompanyId == specParams.Id))
      {
         //AddInclude(e => e.Company);
         AddInclude(e => e.Module);
         //AddInclude(x => x.Module.SecDScreens);
         AddOrderBy(e => e.Module.ModuleName);
         ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

         if (!string.IsNullOrEmpty(specParams.Sort))
         {
            switch (specParams.Sort)
            {
               case "nameAsc":
                  AddOrderBy(e => e.Company.CompanyName); break;

               case "nameDesc":
                  AddOrderByDescending(e => e.Company.CompanyName); break;

               default:
                  AddOrderBy(e => e.Company.CompanyName); break;
            }
         }
      }
      public SoftConfigJCompanyLinkModuleSpecification(int id) : base(e => e.CompanyId == id)
      {
         AddInclude(e => e.Module);
         //AddInclude(x => x.EduBClassOrHallRoomInfos);
      }
   }

   public class SoftConfigJCompanyLinkModuleWithFiltersForCountSpecification : BaseSpecification<AGenConfigJCompanyLinkModule>
   {
      public SoftConfigJCompanyLinkModuleWithFiltersForCountSpecification(SpecificationParams specParams) : base(x =>
          (string.IsNullOrEmpty(specParams.Search) || x.Company.CompanyName.ToLower().Contains(specParams.Search)) &&
          (!specParams.Id.HasValue || x.CompanyId == specParams.Id))
      {
      }
   }
}
using pbERP.Domain.Models.BSecurity;

namespace pbERP.Infrastructure.Specifications.SectionModule;

public class BSecAUserGroupSpecification : BaseSpecification<BSecAUserGroup>
{
   public BSecAUserGroupSpecification(SpecificationParams specParams) : base(x =>
   string.IsNullOrEmpty(specParams.Search) || x.UserGroupName.ToLower().Contains(specParams.Search))
   {
      AddOrderBy(s => s.UserGroupName);
      ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

      if (!string.IsNullOrEmpty(specParams.Sort))
      {
         switch (specParams.Sort)
         {
            case "nameAsc": AddOrderBy(s => s.UserGroupName); break;
            case "nameDesc": AddOrderByDescending(s => s.UserGroupName); break;
            default: AddOrderBy(s => s.UserGroupName); break;
         }
      }
   }

   public BSecAUserGroupSpecification(long id) : base(e => e.UserGroupId == id)
   {
      //AddInclude(ug => ug.SecBUsers);
      //AddInclude(ug => ug.SecELinkUserGroupScreens);
      //AddInclude(ug => ug.SecGLinkUserGroupScreenCommands);
   }
}

public class BSecAUserGroupCount : BaseSpecification<BSecAUserGroup>
{
   public BSecAUserGroupCount(SpecificationParams specParams) : base(x =>
     string.IsNullOrEmpty(specParams.Search) || x.UserGroupName.ToLower().Contains(specParams.Search))
   {

   }
}

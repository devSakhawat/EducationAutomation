using pbERP.Domain.Models.BSecurity;

namespace pbERP.Infrastructure.Specifications.SecurityModule;

public class BSecELinkUserGroupScreenSpecification : BaseSpecification<BSecELinkUserGroupScreen>
{
   public BSecELinkUserGroupScreenSpecification(SpecificationParams specParams) : base(x =>
   string.IsNullOrEmpty(specParams.Search) || x.UserGroup.UserGroupName.ToLower().Contains(specParams.Search))
   {
      AddInclude(x => x.UserGroup);
      AddInclude(x => x.Screen);
      AddOrderByDescending(u => u.UserGroup.UserGroupName);
      ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

      if (!string.IsNullOrEmpty(specParams.Sort))
      {
         switch (specParams.Sort)
         {
            case "nameAsc": AddOrderBy(u => u.UserGroup.UserGroupName); break;
            case "nameDesc": AddOrderByDescending(u => u.Screen.ScreenName); break;
            default: AddOrderBy(u => u.UserGroup.UserGroupName); break;
         }
      }
   }

   public BSecELinkUserGroupScreenSpecification(long id) : base(u => u.LinkUserGroupScreenId == id)
   {
      AddInclude(x => x.UserGroup);
      AddInclude(x => x.Screen);
   }
}

public class BSecELinkUserGroupScreenCount : BaseSpecification<BSecELinkUserGroupScreen>
{
   public BSecELinkUserGroupScreenCount(SpecificationParams specParams) : base(x =>
   string.IsNullOrEmpty(specParams.Search) || x.UserGroup.UserGroupName.ToLower().Contains(specParams.Search))
   {

   }
}

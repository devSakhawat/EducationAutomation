using pbERP.Domain.Models.BSecurity;

namespace pbERP.Infrastructure.Specifications.SecurityModule;

public class BSecBUserSpecification : BaseSpecification<BSecBUser>
{
   public BSecBUserSpecification(SpecificationParams specParams) : base(x =>
   string.IsNullOrEmpty(specParams.Search) || x.LoginName.ToLower().Contains(specParams.Search))
   {
      AddInclude(x => x.UserGroup);
      AddOrderByDescending(u => u.UserId);
      ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

      if (!string.IsNullOrEmpty(specParams.Sort))
      {
         switch (specParams.Sort)
         {
            case "nameAsc": AddOrderBy(u => u.LoginName); break;
            case "nameDesc": AddOrderByDescending(u => u.LoginName); break;
            default: AddOrderBy(u => u.LoginName); break;
         }
      }
   }

   public BSecBUserSpecification(long id) : base(u => u.UserId == id)
   {
      AddInclude(x => x.UserGroup);
   }
}

public class BSecBUserCountSpecification : BaseSpecification<BSecBUser>
{
   public BSecBUserCountSpecification(SpecificationParams specParams) : base(x =>
   string.IsNullOrEmpty(specParams.Search) || x.LoginName.ToLower().Contains(specParams.Search))
   {

   }
}

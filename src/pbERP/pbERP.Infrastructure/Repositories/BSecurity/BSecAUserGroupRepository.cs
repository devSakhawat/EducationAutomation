using pbERP.DataStructure;
using pbERP.Domain.Models.BSecurity;
using pbERP.Infrastructure.Constracts.BSecurity;

namespace pbERP.Infrastructure.Repositories.BSecurity;

public class BSecAUserGroupRepository : GenericRepository<BSecAUserGroup>, IBSecAUserGroupRepository
{

   public BSecAUserGroupRepository(pbERPContext context) : base(context)
   {
   }
}

using pbERP.DataStructure;
using pbERP.Domain.Models.BSecurity;
using pbERP.Infrastructure.Constracts.BSecurity;

namespace pbERP.Infrastructure.Repositories.BSecurity;

public class BSecBUserRepository : GenericRepository<BSecBUser>, IBSecBUserRepository
{
    public BSecBUserRepository(pbERPContext context) : base(context)
    {

    }
}

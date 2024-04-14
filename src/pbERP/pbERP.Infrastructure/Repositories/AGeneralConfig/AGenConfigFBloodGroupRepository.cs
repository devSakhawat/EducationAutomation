using pbERP.DataStructure;
using pbERP.Domain.Models.AGeneralConfig;
using pbERP.Infrastructure.Constracts.AGeneralConfig;

namespace pbERP.Infrastructure.Repositories.AGeneralConfig;

public class AGenConfigFBloodGroupRepository : GenericRepository<AGenConfigFBloodGroup>, IAGenConfigFBloodGroupRepository
{
    public AGenConfigFBloodGroupRepository(pbERPContext Context) : base(Context)
    {
    }
}
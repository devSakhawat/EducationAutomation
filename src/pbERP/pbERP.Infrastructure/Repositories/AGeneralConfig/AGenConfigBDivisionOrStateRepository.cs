using pbERP.DataStructure;
using pbERP.Domain.Models.AGeneralConfig;
using pbERP.Infrastructure.Constracts.AGeneralConfig;

namespace pbERP.Infrastructure.Repositories.AGeneralConfig;

public class AGenConfigBDivisionOrStateRepository : GenericRepository<AGenConfigBDivisionOrState>, IAGenConfigBDivisionOrStateRepository
{
   public AGenConfigBDivisionOrStateRepository(pbERPContext context) : base(context)
   {
   }
}

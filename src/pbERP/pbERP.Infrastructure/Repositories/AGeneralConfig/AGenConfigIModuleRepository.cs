using pbERP.DataStructure;
using pbERP.Domain.Models.AGeneralConfig;
using pbERP.Infrastructure.Constracts.AGeneralConfig;

namespace pbERP.Infrastructure.Repositories.AGeneralConfig;

public class AGenConfigIModuleRepository : GenericRepository<AGenConfigIModule>, IAGenConfigIModuleRepository
{
   public AGenConfigIModuleRepository(pbERPContext context) : base(context)
   {
   }
}

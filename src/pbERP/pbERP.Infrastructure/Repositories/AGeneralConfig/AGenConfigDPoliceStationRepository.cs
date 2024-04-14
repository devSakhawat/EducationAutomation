using pbERP.DataStructure;
using pbERP.Domain.Models.AGeneralConfig;
using pbERP.Infrastructure.Constracts.AGeneralConfig;

namespace pbERP.Infrastructure.Repositories.AGeneralConfig;

public class AGenConfigDPoliceStationRepository : GenericRepository<AGenConfigDPoliceStation>, IAGenConfigDPoliceStationRepository
{
   public AGenConfigDPoliceStationRepository(pbERPContext context) : base(context)
   {
   }
}

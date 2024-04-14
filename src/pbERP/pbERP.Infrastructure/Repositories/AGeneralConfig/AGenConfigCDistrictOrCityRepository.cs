using pbERP.DataStructure;
using pbERP.Domain.Models.AGeneralConfig;
using pbERP.Infrastructure.Constracts.AGeneralConfig;

namespace pbERP.Infrastructure.Repositories.AGeneralConfig;

public class AGenConfigCDistrictOrCityRepository : GenericRepository<AGenConfigCDistrictOrCity>, IAGenConfigCDistrictOrCityRepository
{
   public AGenConfigCDistrictOrCityRepository(pbERPContext context) : base(context)
   {
   }
}

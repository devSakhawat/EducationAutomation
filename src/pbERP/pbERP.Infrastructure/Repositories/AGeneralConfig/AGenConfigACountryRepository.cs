using pbERP.DataStructure;
using pbERP.Domain.Models.AGeneralConfig;
using pbERP.Infrastructure.Constracts.AGeneralConfig;

namespace pbERP.Infrastructure.Repositories.AGeneralConfig;

public class AGenConfigACountryRepository : GenericRepository<AGenConfigACountry>, IAGenConfigACountryRepository
{
   public AGenConfigACountryRepository(pbERPContext context) : base(context)
   {
   }
}

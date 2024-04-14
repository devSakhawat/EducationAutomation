using pbERP.DataStructure;
using pbERP.Domain.Models.AGeneralConfig;
using pbERP.Infrastructure.Constracts.AGeneralConfig;

namespace pbERP.Infrastructure.Repositories.AGeneralConfig;

public class AGenConfigGReligionRepository : GenericRepository<AGenConfigGReligion>, IAGenConfigGReligionRepository
{
   public AGenConfigGReligionRepository(pbERPContext context) : base(context)
   {
   }
}

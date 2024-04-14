using pbERP.DataStructure;
using pbERP.Domain.Models.AGeneralConfig;
using pbERP.Infrastructure.Constracts.AGeneralConfig;

namespace pbERP.Infrastructure.Repositories.AGeneralConfig;

public class AGenConfigEGenderRepository : GenericRepository<AGenConfigEGender>, IAGenConfigEGenderRepository
{
   public AGenConfigEGenderRepository(pbERPContext context) : base(context)
   {
   }
}

using pbERP.DataStructure;
using pbERP.Domain.Models.FEducation;
using pbERP.Infrastructure.Constracts.FEducation;

namespace pbERP.Infrastructure.Repositories.FEducation;

public class FEduBBuildingRepository : GenericRepository<FEduBBuilding>, IFEduBBuildingRepository
{
   public FEduBBuildingRepository(pbERPContext context) : base(context)
   {
   }
}

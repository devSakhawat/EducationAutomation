using pbERP.DataStructure;
using pbERP.Domain.Models.FEducation;
using pbERP.Infrastructure.Constracts.FEducation;

namespace pbERP.Infrastructure.Repositories.FEducation;

public class FEduKClassGroupRepository : GenericRepository<FEduKClassGroup>, IFEduKClassGroupRepository
{
   public FEduKClassGroupRepository(pbERPContext context) : base(context)
   {
   }
}

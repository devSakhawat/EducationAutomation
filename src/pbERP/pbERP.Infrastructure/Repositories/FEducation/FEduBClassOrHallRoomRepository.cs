using pbERP.DataStructure;
using pbERP.Domain.Models.FEducation;
using pbERP.Infrastructure.Constracts.FEducation;

namespace pbERP.Infrastructure.Repositories.FEducation;

public class FEduBClassOrHallRoomRepository : GenericRepository<FEduBClassOrHallRoom>, IFEduBClassOrHallRoomRepository
{
   public FEduBClassOrHallRoomRepository(pbERPContext context) : base(context)
   {
   }
}

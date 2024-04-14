using pbERP.DataStructure;
using pbERP.Domain.Models.FEducation;
using pbERP.Infrastructure.Constracts.FEducation;

namespace pbERP.Infrastructure.Repositories.FEducation;

public class FEduUClassPeriodRepository : GenericRepository<FEduUClassPeriod>, IFEduUClassPeriodRepository
{
   public FEduUClassPeriodRepository(pbERPContext context) : base(context)
   {
   }
}

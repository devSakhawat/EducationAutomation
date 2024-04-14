using pbERP.DataStructure;
using pbERP.Domain.Models.DHR;
using pbERP.Infrastructure.Constracts.HR;

namespace pbERP.Infrastructure.Repositories.Security;

public class HrJEmployeeRepository : GenericRepository<DHrJEmployee>, IHrJEmployeeRepository
{
   public HrJEmployeeRepository(pbERPContext context) : base(context)
   {
   }
}

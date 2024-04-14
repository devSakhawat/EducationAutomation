using pbERP.DataStructure;
using pbERP.Domain.Models.FEducation;
using pbERP.Infrastructure.Constracts.FEducation;

namespace pbERP.Infrastructure.Repositories.FEducation;

public class FEduFTransportChargeRepository : GenericRepository<FEduFTransportCharge>, IFEduFTransportChargeRepository
{
   public FEduFTransportChargeRepository(pbERPContext context) : base(context)
   {
   }
}

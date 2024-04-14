using pbERP.DataStructure;
using pbERP.Domain.Models.DHR;
using pbERP.Infrastructure.Constracts.DHR;

namespace pbERP.Infrastructure.Repositories.Education;

public class DHrLPresentAddressRepository : GenericRepository<DHrLPresentAddress>, IDHrLPresentAddressRepository
{
   public DHrLPresentAddressRepository(pbERPContext context) : base(context)
   {
   }
}
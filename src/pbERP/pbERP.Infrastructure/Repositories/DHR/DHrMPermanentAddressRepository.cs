using pbERP.DataStructure;
using pbERP.Domain.Models.DHR;
using pbERP.Infrastructure.Constracts.DHR;

namespace pbERP.Infrastructure.Repositories.Education;

public class DHrMPermanentAddressRepository : GenericRepository<DHrMPermanentAddress>, IDHrMPermanentAddressRepository
{
   public DHrMPermanentAddressRepository(pbERPContext context) : base(context)
   {
   }
}
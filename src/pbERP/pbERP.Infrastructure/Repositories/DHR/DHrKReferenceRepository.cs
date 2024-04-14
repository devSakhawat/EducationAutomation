using pbERP.DataStructure;
using pbERP.Domain.Models.DHR;
using pbERP.Infrastructure.Constracts.DHR;

namespace pbERP.Infrastructure.Repositories.Education;

public class DHrKReferenceTypeRepository : GenericRepository<DHrKReferenceType>, IDHrKReferenceTypeRepository
{
   public DHrKReferenceTypeRepository(pbERPContext context) : base(context)
   {
   }
}
using pbERP.DataStructure;
using pbERP.Domain.Models.FEducation;
using pbERP.Infrastructure.Constracts.FEducation;

namespace pbERP.Infrastructure.Repositories.FEducation;

public class FEduHStudentAllocateTransportRepository : GenericRepository<FEduHStudentAllocateTransport>, IFEduHStudentAllocateTransportRepository
{
   public FEduHStudentAllocateTransportRepository(pbERPContext context) : base(context)
   {
   }
}

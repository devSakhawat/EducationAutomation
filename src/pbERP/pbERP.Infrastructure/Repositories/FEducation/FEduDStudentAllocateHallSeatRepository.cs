using pbERP.DataStructure;
using pbERP.Domain.Models.FEducation;
using pbERP.Infrastructure.Constracts.FEducation;

namespace pbERP.Infrastructure.Repositories.FEducation;

public class FEduDStudentAllocateHallSeatRepository : GenericRepository<FEduDStudentAllocateHallSeat>, IFEduDStudentAllocateHallSeatRepository
{
   public FEduDStudentAllocateHallSeatRepository(pbERPContext context) : base(context)
   {
   }
}

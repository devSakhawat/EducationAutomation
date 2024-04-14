using pbERP.DataStructure;
using pbERP.Domain.Models.FEducation;
using pbERP.Infrastructure.Constracts.FEducation;

namespace pbERP.Infrastructure.Repositories.FEducation;

public class FEduRExamRepository : GenericRepository<FEduRExam>, IFEduRExamRepository
{
   public FEduRExamRepository(pbERPContext context) : base(context)
   {
   }
}

using pbERP.DataStructure;
using pbERP.Domain.Models.FEducation;
using pbERP.Infrastructure.Constracts.FEducation;

namespace pbERP.Infrastructure.Repositories.FEducation;

public class FEduExamBExamShortCodeRepository : GenericRepository<FEduExamBExamShortCode>, IFEduExamBExamShortCodeRepository
{
   public FEduExamBExamShortCodeRepository(pbERPContext context) : base(context)
   {
   }
}

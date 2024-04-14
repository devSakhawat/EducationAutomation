using pbERP.DataStructure;
using pbERP.Domain.Models.FEducation;
using pbERP.Infrastructure.Constracts.FEducation;

namespace pbERP.Infrastructure.Repositories.FEducation;

public class FEduExamAGradePointRepository : GenericRepository<FEduExamAGradePoint>, IFEduExamAGradePointRepository
{
    public FEduExamAGradePointRepository(pbERPContext context) : base(context)
    {
    }       
}

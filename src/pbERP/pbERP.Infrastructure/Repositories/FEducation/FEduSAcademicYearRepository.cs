using pbERP.DataStructure;
using pbERP.Domain.Models.FEducation;
using pbERP.Infrastructure.Constracts.FEducation;

namespace pbERP.Infrastructure.Repositories.FEducation;

public class FEduSAcademicYearRepository : GenericRepository<FEduSAcademicYear>, IFEduSAcademicYearRepository
{
   public FEduSAcademicYearRepository(pbERPContext context) : base(context)
   {
   }
}

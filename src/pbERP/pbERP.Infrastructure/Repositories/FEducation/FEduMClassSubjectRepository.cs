using pbERP.DataStructure;
using pbERP.Domain.Models.FEducation;
using pbERP.Infrastructure.Constracts.FEducation;

namespace pbERP.Infrastructure.Repositories.FEducation;

public class FEduMClassSubjectRepository : GenericRepository<FEduMClassSubject>, IFEduMClassSubjectRepository
{
   public FEduMClassSubjectRepository(pbERPContext context) : base(context)
   {
   }
}

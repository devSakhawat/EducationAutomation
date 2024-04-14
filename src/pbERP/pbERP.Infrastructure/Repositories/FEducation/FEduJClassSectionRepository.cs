using pbERP.DataStructure;
using pbERP.Domain.Models.FEducation;
using pbERP.Infrastructure.Constracts.FEducation;

namespace pbERP.Infrastructure.Repositories.FEducation;

public class FEduJClassSectionRepository : GenericRepository<FEduJClassSection>, IFEduJClassSectionRepository
{
   public FEduJClassSectionRepository(pbERPContext context) : base(context)
   {
   }
}

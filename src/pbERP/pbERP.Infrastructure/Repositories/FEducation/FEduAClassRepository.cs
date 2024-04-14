using pbERP.DataStructure;
using pbERP.Domain.Models.FEducation;
using pbERP.Infrastructure.Constracts.FEducation;

namespace pbERP.Infrastructure.Repositories.FEducation;

public class FEduAClassRepository : GenericRepository<FEduAClass>, IFEduAClassRepository
{
    public FEduAClassRepository(pbERPContext context) : base(context)
    {
    }
}

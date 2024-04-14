using pbERP.DataStructure;
using pbERP.Domain.Models.FEducation;
using pbERP.Infrastructure.Constracts.FEducation;

namespace pbERP.Infrastructure.Repositories.FEducation;

public class FEduLClassShiftRepository : GenericRepository<FEduLClassShift>, IFEduLClassShiftRepository
{
    public FEduLClassShiftRepository(pbERPContext context) : base(context)
    {
    }
}

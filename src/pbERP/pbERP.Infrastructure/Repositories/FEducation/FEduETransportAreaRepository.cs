using pbERP.DataStructure;
using pbERP.Domain.Models.FEducation;
using pbERP.Infrastructure.Constracts.FEducation;

namespace pbERP.Infrastructure.Repositories.FEducation;

public class FEduETransportAreaRepository : GenericRepository<FEduETransportArea>, IFEduETransportAreaRepository
{
   public FEduETransportAreaRepository(pbERPContext context) : base(context)
   {
   }
}

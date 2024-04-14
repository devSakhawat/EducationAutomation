using pbERP.DataStructure;
using pbERP.Domain.Models.BSecurity;
using pbERP.Infrastructure.Constracts.BSecurity;

namespace pbERP.Infrastructure.Repositories.BSecurity;

public class BSecDScreenRepository : GenericRepository<BSecDScreen>, IBSecDScreenRepository
{
   public BSecDScreenRepository(pbERPContext context) : base(context)
   {
   }
}

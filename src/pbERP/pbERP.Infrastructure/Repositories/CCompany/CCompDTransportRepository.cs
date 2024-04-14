using pbERP.DataStructure;
using pbERP.Domain.Models.CCompany;
using pbERP.Infrastructure.Constracts.CCompany;

namespace pbERP.Infrastructure.Repositories.CCompany;

public class CCompDTransportRepository : GenericRepository<CCompDTransport>, ICCompDTransportRepository
{
    public CCompDTransportRepository(pbERPContext _context) : base(_context)
    {
        
    }
}

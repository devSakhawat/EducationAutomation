using Microsoft.EntityFrameworkCore;
using pbERP.DataStructure;
using pbERP.Domain.DTOs;
using pbERP.Domain.DTOs.FEducation;
using pbERP.Domain.Enums;
using pbERP.Domain.Models.AGeneralConfig;
using pbERP.Domain.Models.CCompany;
using pbERP.Domain.Models.DHR;
using pbERP.Domain.Models.FEducation;
using pbERP.Infrastructure.Constracts.CCompany;
using pbERP.Utilities.Constant;

namespace pbERP.Infrastructure.Repositories.CCompany;

public class CCompACompanyRepository : GenericRepository<CCompACompany>, ICCompACompanyRepository
{
    public CCompACompanyRepository(pbERPContext _context) : base(_context)
    {
    }
}
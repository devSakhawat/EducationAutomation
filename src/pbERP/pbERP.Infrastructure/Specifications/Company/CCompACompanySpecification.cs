using pbERP.Domain.Models.CCompany;
using pbERP.Domain.Models.FEducation;

namespace pbERP.Infrastructure.Specifications.Company;

public class CCompACompanySpecification : BaseSpecification<CCompACompany>
{
    public CCompACompanySpecification(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) || x.CompanyCode.ToLower().Contains(specParams.Search))
    {
        AddInclude(x => x.BusinessType);
        AddInclude(x => x.PoliceStation);

        AddOrderByDescending(s => s.CompanyId);
        ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

        if (!string.IsNullOrEmpty(specParams.Sort))
        {
            switch (specParams.Sort)
            {
                case "nameAsc": AddOrderBy(s => s.CompanyName); break;
                case "nameDesc": AddOrderByDescending(s => s.CompanyName); break;
                default: AddOrderBy(s => s.CompanyCode); break;
            }
        }
    }
    public CCompACompanySpecification(long id) : base(e => e.CompanyId == id)
    {
        AddInclude(x => x.BusinessType);
        AddInclude(x => x.PoliceStation);
    }
}

public class CCompACompanysWithFiltersForCount : BaseSpecification<CCompACompany>
{
    public CCompACompanysWithFiltersForCount(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) || x.CompanyCode.ToLower().Contains(specParams.Search))
    {
    }
}

public class CCompACompanyDelete : BaseSpecification<CCompACompany>
{
    public CCompACompanyDelete(long id) : base(x => x.CompanyId == id)
    {
        AddInclude(x => x.AGenConfigJCompanyLinkModules);
        AddInclude(x => x.CCompBBranches);
        AddInclude(x => x.CCompCTransportTypes);
        AddInclude(x => x.FEduBBuildings);
    }
}
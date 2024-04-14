using pbERP.Domain.Models.CCompany;

namespace pbERP.Infrastructure.Specifications.Company;

public class CCompDTransportSpecification : BaseSpecification<CCompDTransport>
{
   public CCompDTransportSpecification(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) ||
   x.TransportName.ToLower().Contains(specParams.Search))
   {
      AddInclude(x => x.TransportType);

      AddOrderByDescending(x => x.TransportId);
      ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

      if (!string.IsNullOrEmpty(specParams.Search))
      {
         switch (specParams.Sort)
         {
            case "nameAsc": AddOrderBy(x => x.TransportName); break;
            case "nameDesc": AddOrderByDescending(x => x.TransportName); break;
            default: AddOrderBy(x => x.TransportId); break;
         }
      }
   }

   public CCompDTransportSpecification(long id) : base(x => x.TransportId == id)
   {
      AddInclude(x => x.TransportType);
   }
}

public class CCompDTransportWithFiltersForCount : BaseSpecification<CCompDTransport>
{
   public CCompDTransportWithFiltersForCount(SpecificationParams specParams) : base(x =>
   string.IsNullOrEmpty(specParams.Search) || x.TransportName.ToLower().Contains(specParams.Search))
   {

   }

}

public class CCompDTransportDelete : BaseSpecification<CCompDTransport>
{
   public CCompDTransportDelete(long id) : base(x => x.TransportId == id)
   {
      AddInclude(x => x.FEduFTransportCharges);
      AddInclude(x => x.FEduGLinkTransportAreas);
      AddInclude(x => x.FEduHStudentAllocateTransports);
   }
}

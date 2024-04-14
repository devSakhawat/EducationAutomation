using pbERP.Domain.Models.FEducation;

namespace pbERP.Infrastructure.Specifications.Education
{
  public class FEduGLinkTransportAreaSpecification : BaseSpecification<FEduGLinkTransportArea>
  {
    public FEduGLinkTransportAreaSpecification(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) || x.TransportId.ToString().ToLower().Contains(specParams.Search))
    {
      AddInclude(x => x.Transport);
      AddInclude(x => x.TransportArea);
      AddOrderByDescending(s => s.LinkAreaTransportId);
      ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

      if (!string.IsNullOrEmpty(specParams.Sort))
      {
        switch (specParams.Sort)
        {
          case "nameAsc": AddOrderBy(s => s.TransportAreaId); break;
          case "nameDesc": AddOrderByDescending(s => s.TransportAreaId); break;
          default: AddOrderBy(s => s.LinkAreaTransportId); break;
        }
      }
    }

    public FEduGLinkTransportAreaSpecification(long id) : base(e => e.LinkAreaTransportId == id)
    {
      AddInclude(x => x.Transport);
      AddInclude(x => x.TransportArea);
    }
  }

  public class FEduGLinkTransportAreaCount : BaseSpecification<FEduGLinkTransportArea>
  {
    public FEduGLinkTransportAreaCount(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) || x.TransportId.ToString().ToLower().Contains(specParams.Search))
    {
    }
  }

  public class FEduGLinkTransportAreaDelete : BaseSpecification<FEduGLinkTransportArea>
  {
    public FEduGLinkTransportAreaDelete(long id) : base(x => x.LinkAreaTransportId == id)
    {
    }
  }
}
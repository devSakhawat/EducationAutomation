using pbERP.Domain.Models.FEducation;

namespace pbERP.Infrastructure.Specifications.Education;

public class FEduTAcademicSessionSpecification : BaseSpecification<FEduTAcademicSession>
{
   public FEduTAcademicSessionSpecification(SpecificationParams specParams)
     : base(x => string.IsNullOrEmpty(specParams.Search) || x.AcademicSession.ToLower().Contains(specParams.Search))
   {
      AddOrderByDescending(x => x.AcademicSessionId);
      ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

      if (!string.IsNullOrEmpty(specParams.Sort))
      {
         switch (specParams.Sort)
         {
            case "nameAsc": AddOrderBy(x => x.AcademicSession); break;
            case "nameDesc": AddOrderByDescending(x => x.AcademicSession); break;
            default: AddOrderByDescending(x => x.AcademicSessionId); break;
         }
      }
   }

   public FEduTAcademicSessionSpecification(long id) : base(e => e.AcademicSessionId == id)
   {

   }
}

public class FEduTAcademicSessionCount : BaseSpecification<FEduTAcademicSession>
{
   public FEduTAcademicSessionCount(SpecificationParams specParams)
     : base(x => string.IsNullOrEmpty(specParams.Search) || x.AcademicSession.ToLower().Contains(specParams.Search))
   {

   }
}

public class FEduTAcademicSessionDelete : BaseSpecification<FEduTAcademicSession>
{
    public FEduTAcademicSessionDelete(long id) : base(x => x.AcademicSessionId == id)
    {
        
    }
}

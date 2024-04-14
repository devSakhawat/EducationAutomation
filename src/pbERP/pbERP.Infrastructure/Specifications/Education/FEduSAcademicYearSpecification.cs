using pbERP.Domain.Models.FEducation;

namespace pbERP.Infrastructure.Specifications.Education;

public class FEduSAcademicYearSpecification : BaseSpecification<FEduSAcademicYear>
{
   public FEduSAcademicYearSpecification(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) || x.AcademicYear.ToLower().Contains(specParams.Search))
   {
      AddOrderByDescending(s => s.AcademicYearId);
      ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

      if (!string.IsNullOrEmpty(specParams.Sort))
      {
         switch (specParams.Sort)
         {
            case "nameAsc": AddOrderBy(x => x.AcademicYear); break;
            case "nameDesc": AddOrderByDescending(x => x.AcademicYear); break;
            default: AddOrderByDescending(x => x.AcademicYearId); break;
         }
      }
   }
   public FEduSAcademicYearSpecification(long id) : base(e => e.AcademicYearId == id)
   {
   }
}

public class FEduSAcademicYearCount : BaseSpecification<FEduSAcademicYear>
{
   public FEduSAcademicYearCount(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) || x.AcademicYear.ToLower().Contains(specParams.Search))
   {
   }
}

public class FEduSAcademicYearDelete : BaseSpecification<FEduSAcademicYear>
{
   public FEduSAcademicYearDelete(long id) : base(x => x.AcademicYearId == id)
   {
   }
}

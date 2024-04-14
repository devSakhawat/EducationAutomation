using pbERP.Domain.Models.FEducation;

namespace pbERP.Infrastructure.Specifications.Education;

public class FEduAStudentSpecification : BaseSpecification<FEduAStudent>
{
   public FEduAStudentSpecification(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) || x.StudentCode.ToLower().Contains(specParams.Search))
   {
      AddInclude(x => x.BloodGroup);
      AddInclude(x => x.Gender);
      AddInclude(x => x.Religion);

      AddOrderByDescending(s => s.StudentId);
      ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

      if (!string.IsNullOrEmpty(specParams.Sort))
      {
         switch (specParams.Sort)
         {
            case "nameAsc": AddOrderBy(s => s.StudentName); break;
            case "nameDesc": AddOrderByDescending(s => s.StudentName); break;
            default:AddOrderBy(s => s.StudentCode); break;
         }
      }
   }
   public FEduAStudentSpecification(long id) : base(e => e.StudentId == id)
   {
      AddInclude(x => x.BloodGroup);
      AddInclude(x => x.Gender);
      AddInclude(x => x.Religion);
   }
}

public class FEduAStudentsWithFiltersForCount : BaseSpecification<FEduAStudent>
{
   public FEduAStudentsWithFiltersForCount(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) || x.StudentCode.ToLower().Contains(specParams.Search))
   {
   }
}

public class FEduAStudentDelete : BaseSpecification<FEduAStudent>
{
   public FEduAStudentDelete(long id) : base(x => x.StudentId == id)
   {
   }
}
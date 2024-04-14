using pbERP.Domain.Models.FEducation;

namespace pbERP.Infrastructure.Specifications.Education;

public class FEduRExamSpecification : BaseSpecification<FEduRExam>
{
   public FEduRExamSpecification(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) || x.ExamName.ToLower().Contains(specParams.Search))
   {
      AddOrderByDescending(s => s.ExamId);
      ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

      if (!string.IsNullOrEmpty(specParams.Sort))
      {
         switch (specParams.Sort)
         {
            case "nameAsc": AddOrderBy(x => x.ExamName); break;
            case "nameDesc": AddOrderByDescending(x => x.ExamName); break;
            default: AddOrderByDescending(x => x.ExamId); break;
         }
      }
   }
   public FEduRExamSpecification(long id) : base(e => e.ExamId == id)
   {
   }
}

public class FEduRExamCount : BaseSpecification<FEduRExam>
{
   public FEduRExamCount(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) || x.ExamName.ToLower().Contains(specParams.Search))
   {
   }
}

public class FEduRExamDelete : BaseSpecification<FEduRExam>
{
   public FEduRExamDelete(long id) : base(x => x.ExamId == id)
   {
   }
}

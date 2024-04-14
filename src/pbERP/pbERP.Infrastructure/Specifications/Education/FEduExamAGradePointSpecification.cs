using pbERP.Domain.Models.FEducation;

namespace pbERP.Infrastructure.Specifications.Education;

public class FEduExamAGradePointSpecification : BaseSpecification<FEduExamAGradePoint>
{
  public FEduExamAGradePointSpecification(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) || x.LetterGrade.ToLower().Contains(specParams.Search))
  {
    AddOrderByDescending(x => x.GradePointId);
    ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
    if (!string.IsNullOrEmpty(specParams.Sort))
    {
      switch (specParams.Sort)
      {
        case "nameAsc": AddOrderBy(x => x.LetterGrade); break;
        case "nameDesc": AddOrderByDescending(x => x.LetterGrade); break;
        default: AddOrderByDescending(x => x.GradePointId); break;
      }
    }
  }

  public FEduExamAGradePointSpecification(long id) : base(x => x.GradePointId == id)
  {
  }
}

public class FEduExamAGradePointCount : BaseSpecification<FEduExamAGradePoint>
{
  public FEduExamAGradePointCount(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) || x.LetterGrade.ToLower().Contains(specParams.Search))
  {
  }
}

public class FEduExamAGradePointDelete : BaseSpecification<FEduExamAGradePoint>
{
  public FEduExamAGradePointDelete(long id) : base(x => x.GradePointId == id)
  {

  }
}

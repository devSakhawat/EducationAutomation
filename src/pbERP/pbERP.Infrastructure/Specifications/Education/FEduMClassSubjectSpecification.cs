using pbERP.Domain.Models.FEducation;

namespace pbERP.Infrastructure.Specifications.Education;

public class FEduMClassSubjectSpecification : BaseSpecification<FEduMClassSubject>
{
   public FEduMClassSubjectSpecification(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) || x.ClassSubjectName.ToLower().Contains(specParams.Search))
   {
      AddOrderByDescending(s => s.ClassSubjectId);
      ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

      if (!string.IsNullOrEmpty(specParams.Sort))
      {
         switch (specParams.Sort)
         {
            case "nameAsc": AddOrderBy(x => x.ClassSubjectName); break;
            case "nameDesc": AddOrderByDescending(x => x.ClassSubjectName); break;
            default: AddOrderByDescending(x => x.ClassSubjectId); break;
         }
      }
   }
   public FEduMClassSubjectSpecification(long id) : base(e => e.ClassSubjectId == id)
   {
   }
}

public class FEduMClassSubjectCount : BaseSpecification<FEduMClassSubject>
{
   public FEduMClassSubjectCount(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) || x.ClassSubjectName.ToLower().Contains(specParams.Search))
   {
   }
}

public class FEduMClassSubjectDelete : BaseSpecification<FEduMClassSubject>
{
   public FEduMClassSubjectDelete(long id) : base(x => x.ClassSubjectId == id)
   {
      AddInclude(x => x.FEduQLinkClassSubjects);
   }
}

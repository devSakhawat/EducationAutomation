using pbERP.Domain.Models.FEducation;

namespace pbERP.Infrastructure.Specifications.Education;

public class FEduJClassSectionSpecification : BaseSpecification<FEduJClassSection>
{
   public FEduJClassSectionSpecification(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) || x.ClassSectionName.ToLower().Contains(specParams.Search))
   {
      AddOrderByDescending(s => s.ClassSectionId);
      ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

      if (!string.IsNullOrEmpty(specParams.Sort))
      {
         switch (specParams.Sort)
         {
            case "nameAsc": AddOrderBy(s => s.ClassSectionName); break;

            case "nameDesc": AddOrderByDescending(s => s.ClassSectionName); break;

            default: AddOrderBy(s => s.ClassSectionId); break;
         }
      }
   }
   public FEduJClassSectionSpecification(long id) : base(e => e.ClassSectionId == id)
   {
   }
}

public class FEduJClassSectionCount : BaseSpecification<FEduJClassSection>
{
   public FEduJClassSectionCount(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) || x.ClassSectionName.ToLower().Contains(specParams.Search))
   {
   }
}

public class FEduJClassSectionDelete : BaseSpecification<FEduJClassSection>
{
   public FEduJClassSectionDelete(long id) : base(x => x.ClassSectionId == id)
   {
   }
}


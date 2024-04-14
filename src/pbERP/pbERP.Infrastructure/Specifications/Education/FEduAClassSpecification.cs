using pbERP.Domain.Models.FEducation;

namespace pbERP.Infrastructure.Specifications.Education;

public class FEduAClassSpecification : BaseSpecification<FEduAClass>
{
   public FEduAClassSpecification(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) || x.ClassName.ToLower().Contains(specParams.Search))
   {
      AddOrderByDescending(s => s.ClassId);
      ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

      if (!string.IsNullOrEmpty(specParams.Sort))
      {
         switch (specParams.Sort)
         {
            case "nameAsc": AddOrderBy(x => x.ClassName); break;
            case "nameDesc": AddOrderByDescending(x => x.ClassName); break;
            default: AddOrderByDescending(x => x.ClassId); break;
         }
      }
   }
   public FEduAClassSpecification(long id) : base(e => e.ClassId == id)
   {
   }
}

public class FEduAClassCount : BaseSpecification<FEduAClass>
{
   public FEduAClassCount(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) || x.ClassName.ToLower().Contains(specParams.Search))
   {
   }
}

public class FEduAClassDelete : BaseSpecification<FEduAClass>
{
   public FEduAClassDelete(long id) : base(x => x.ClassId == id)
   {
      AddInclude(x => x.FEduNLinkClassGroups);
      AddInclude(x => x.FEduOLinkClassSections);
      AddInclude(x => x.FEduQLinkClassSubjects);
   }
}

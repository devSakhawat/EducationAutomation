using pbERP.Domain.Models.FEducation;

namespace pbERP.Infrastructure.Specifications.Education;

public class FEduKClassGroupSpecification : BaseSpecification<FEduKClassGroup>
{
   public FEduKClassGroupSpecification(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) || x.ClassGroupName.ToLower().Contains(specParams.Search))
   {
      AddOrderByDescending(s => s.ClassGroupId);
      ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

      if (!string.IsNullOrEmpty(specParams.Sort))
      {
         switch (specParams.Sort)
         {
            case "nameAsc": AddOrderBy(x => x.ClassGroupName); break;
            case "nameDesc": AddOrderByDescending(x => x.ClassGroupName); break;
            default: AddOrderByDescending(x => x.ClassGroupId); break;
         }
      }
   }
   public FEduKClassGroupSpecification(long id) : base(e => e.ClassGroupId == id)
   {
   }
}

public class FEduKClassGroupCount : BaseSpecification<FEduKClassGroup>
{
   public FEduKClassGroupCount(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) || x.ClassGroupName.ToLower().Contains(specParams.Search))
   {
   }
}

public class FEduKClassGroupDelete : BaseSpecification<FEduKClassGroup>
{
   public FEduKClassGroupDelete(long id) : base(x => x.ClassGroupId == id)
   {
      AddInclude(x => x.FEduNLinkClassGroups);
      AddInclude(x => x.FEduQLinkClassSubjects);
   }
}
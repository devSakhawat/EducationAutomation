using pbERP.Domain.Models.AGeneralConfig;

namespace pbERP.Infrastructure.Specifications.GeneralConfig;

public class AGenConfigFBloodGroupSpecification : BaseSpecification<AGenConfigFBloodGroup>
{
    public AGenConfigFBloodGroupSpecification(SpecificationParams speckParams) : base (x => string.IsNullOrEmpty(speckParams.Search) || x.BloodGroupName.ToLower().Contains(speckParams.Search))
    {
      AddOrderByDescending(x => x.BloodGroupId);
      ApplyPaging(speckParams.PageSize * (speckParams.PageIndex - 1), speckParams.PageSize);

      if (!string.IsNullOrEmpty(speckParams.Sort))
      {
         switch(speckParams.Sort)
         {
            case "nameAsc": AddOrderBy(x => x.BloodGroupName); break;
            case "nameDesc": AddOrderByDescending(x => x.BloodGroupName); break;
            default: AddOrderByDescending(x => x.BloodGroupId); break;
         }
      }
    }

   public AGenConfigFBloodGroupSpecification(long id) : base(x => x.BloodGroupId == id )
   {
      
   }
}

public class AGenConfigFBloodGroupCount : BaseSpecification<AGenConfigFBloodGroup>
{
    public AGenConfigFBloodGroupCount(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) || x.BloodGroupName.ToLower().Contains(specParams.Search))
    {
        
    }
}

public class AGenConfigFBloodGroupDelete : BaseSpecification<AGenConfigFBloodGroup>
{
    public AGenConfigFBloodGroupDelete(long id) : base(u => u.BloodGroupId == id)
   {
      AddInclude(x => x.DHrJEmployees);
      AddInclude(x => x.FEduAStudents);
   }
}
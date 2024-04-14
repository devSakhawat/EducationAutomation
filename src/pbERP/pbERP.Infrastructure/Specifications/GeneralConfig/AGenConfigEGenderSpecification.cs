using pbERP.Domain.Models.AGeneralConfig;

namespace pbERP.Infrastructure.Specifications.GeneralConfig;

public class AGenConfigEGenderSpecification : BaseSpecification<AGenConfigEGender>
{
    public AGenConfigEGenderSpecification(SpecificationParams speckParams) : base (x => string.IsNullOrEmpty(speckParams.Search) || x.GenderName.ToLower().Contains(speckParams.Search))
    {
      AddOrderByDescending(x => x.GenderId);
      ApplyPaging(speckParams.PageSize * (speckParams.PageIndex - 1), speckParams.PageSize);

      if (!string.IsNullOrEmpty(speckParams.Sort))
      {
         switch(speckParams.Sort)
         {
            case "nameAsc": AddOrderBy(x => x.GenderName); break;
            case "nameDesc": AddOrderByDescending(x => x.GenderName); break;
            default: AddOrderByDescending(x => x.GenderId); break;
         }
      }
    }

   public AGenConfigEGenderSpecification(long id) : base(x => x.GenderId == id )
   {
   }
}

public class AGenConfigEGenderCount : BaseSpecification<AGenConfigEGender>
{
    public AGenConfigEGenderCount(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) || x.GenderName.ToLower().Contains(specParams.Search))
    {
    }
}

public class AGenConfigEGenderDelete : BaseSpecification<AGenConfigEGender>
{
   public AGenConfigEGenderDelete(long id) : base(u => u.GenderId == id)
   {
      AddInclude(x => x.DHrJEmployees);
      AddInclude(x => x.FEduAStudents);
   }
}
using pbERP.Domain.Models.DHR;

namespace pbERP.Infrastructure.Specifications.HR;

public class HrJEmployeeSpecification : BaseSpecification<DHrJEmployee>
{
   public HrJEmployeeSpecification(SpecificationParams specParams) : base(x =>
     string.IsNullOrEmpty(specParams.Search) || x.EmployeeName.ToLower().Contains(specParams.Search))
   {
      AddOrderByDescending(e => e.EmployeeId);
      ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
      //AddInclude(e => e.Religion);
      if (!string.IsNullOrEmpty(specParams.Sort))
      {
         switch (specParams.Sort)
         {
            case "nameAsc": AddOrderBy(e => e.EmployeeName); break;
            case "nameDesc": AddOrderByDescending(e => e.EmployeeName); break;
            default: AddOrderByDescending(e => e.EmployeeId); break;
         }
      }
   }

   public HrJEmployeeSpecification(long id) : base(e => e.EmployeeId == id)
   {
      //AddInclude(e => e.BloodGroup);
      //AddInclude(e => e.Gender);
      AddInclude(e => e.EmployeeJobRefNo);
      //AddInclude(e => e.Religion);
   }
}

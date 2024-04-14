using pbERP.Domain.Models.DHR;

namespace pbERP.Infrastructure.Specifications.HR;

public class DHrLPresentAddressSpecification : BaseSpecification<DHrLPresentAddress>
{
   public DHrLPresentAddressSpecification(SpecificationParams speckParams) : base(x => string.IsNullOrEmpty(speckParams.Search) || x.PresentAddress.ToLower().Contains(speckParams.Search))
   {
      AddInclude(x => x.PoliceStation);
      AddInclude(x => x.ReferenceTypeId);
      AddOrderByDescending(x => x.PresentAddressId);
      ApplyPaging(speckParams.PageSize * (speckParams.PageIndex - 1), speckParams.PageSize);

      if (!string.IsNullOrEmpty(speckParams.Sort))
      {
         switch (speckParams.Sort)
         {
            case "nameAsc": AddOrderBy(x => x.PresentAddress); break;
            case "nameDesc": AddOrderByDescending(x => x.PresentAddress); break;
            default: AddOrderByDescending(x => x.PresentAddress); break;
         }
      }
   }

   //public DHrLPresentAddressSpecification(long id) : base(x => x.PresentAddressId == id)
   //{
   //   AddInclude(x => x.PoliceStation);
   //   AddInclude(x => x.ReferenceTypeId);
   //}

   public DHrLPresentAddressSpecification(long id) : base(x => x.ReferenceId == id)
   {
      AddInclude(x => x.PoliceStation);
      AddInclude(x => x.ReferenceTypeId);
   }
}

public class DHrLPresentAddressCount : BaseSpecification<DHrLPresentAddress>
{
   public DHrLPresentAddressCount(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) || x.PresentAddress.ToLower().Contains(specParams.Search))
   {
   }
}

public class DHrLPresentAddressDelete : BaseSpecification<DHrLPresentAddress>
{
   public DHrLPresentAddressDelete(long id) : base(u => u.PresentAddressId == id)
   {

   }
}
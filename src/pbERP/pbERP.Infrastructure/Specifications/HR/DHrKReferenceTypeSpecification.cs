using pbERP.Domain.Models.DHR;

namespace pbERP.Infrastructure.Specifications.HR;

public class DHrKReferenceTypeSpecification : BaseSpecification<DHrKReferenceType>
{
   public DHrKReferenceTypeSpecification(SpecificationParams speckParams) : base(x => string.IsNullOrEmpty(speckParams.Search) || x.ReferenceTypeName.ToLower().Contains(speckParams.Search))
   {
      AddOrderByDescending(x => x.ReferenceTypeId);
      ApplyPaging(speckParams.PageSize * (speckParams.PageIndex - 1), speckParams.PageSize);

      if (!string.IsNullOrEmpty(speckParams.Sort))
      {
         switch (speckParams.Sort)
         {
            case "nameAsc": AddOrderBy(x => x.ReferenceTypeName); break;
            case "nameDesc": AddOrderByDescending(x => x.ReferenceTypeName); break;
            default: AddOrderByDescending(x => x.ReferenceTypeId); break;
         }
      }
   }

   public DHrKReferenceTypeSpecification(long id) : base(x => x.ReferenceTypeId == id)
   {
   }
}

public class DHrKReferenceTypeCount : BaseSpecification<DHrKReferenceType>
{
   public DHrKReferenceTypeCount(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) || x.ReferenceTypeName.ToLower().Contains(specParams.Search))
   {
   }
}

public class DHrKReferenceTypeDelete : BaseSpecification<DHrKReferenceType>
{
   public DHrKReferenceTypeDelete(long id) : base(u => u.ReferenceTypeId == id)
   {
      AddInclude(x => x.DHrLPresentAddresses);
      AddInclude(x => x.DHrMPermanentAddresses);
   }
}
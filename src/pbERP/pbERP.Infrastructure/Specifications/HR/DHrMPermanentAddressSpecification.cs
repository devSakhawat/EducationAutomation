using pbERP.Domain.Models.DHR;

namespace pbERP.Infrastructure.Specifications.HR;

public class DHrMPermanentAddressSpecification : BaseSpecification<DHrMPermanentAddress>
{
   public DHrMPermanentAddressSpecification(SpecificationParams speckParams) : base(x => string.IsNullOrEmpty(speckParams.Search) || x.PermanentAddress.ToLower().Contains(speckParams.Search))
   {
      AddInclude(x => x.PoliceStation);
      AddInclude(x => x.ReferenceTypeId);
      AddOrderByDescending(x => x.PermanentAddressId);
      ApplyPaging(speckParams.PageSize * (speckParams.PageIndex - 1), speckParams.PageSize);

      if (!string.IsNullOrEmpty(speckParams.Sort))
      {
         switch (speckParams.Sort)
         {
            case "nameAsc": AddOrderBy(x => x.PermanentAddress); break;
            case "nameDesc": AddOrderByDescending(x => x.PermanentAddress); break;
            default: AddOrderByDescending(x => x.PermanentAddressId); break;
         }
      }
   }

   //public DHrMPermanentAddressSpecification(long id) : base(x => x.PermanentAddressId == id)
   //{
   //   AddInclude(x => x.PoliceStation);
   //   AddInclude(x => x.ReferenceTypeId);
   //}

   public DHrMPermanentAddressSpecification(long id) : base(x => x.ReferenceId == id)
   {
      AddInclude(x => x.PoliceStation);
      AddInclude(x => x.ReferenceTypeId);
   }
}

public class DHrMPermanentAddressCount : BaseSpecification<DHrMPermanentAddress>
{
   public DHrMPermanentAddressCount(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) || x.PermanentAddress.ToLower().Contains(specParams.Search))
   {
   }
}

public class DHrMPermanentAddressDelete : BaseSpecification<DHrMPermanentAddress>
{
   public DHrMPermanentAddressDelete(long id) : base(u => u.PermanentAddressId == id)
   {

   }
}
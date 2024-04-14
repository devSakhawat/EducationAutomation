using pbERP.Domain.Models.FEducation;

namespace pbERP.Infrastructure.Specifications.Education
{
   public class FEduFTransportChargeSpecification : BaseSpecification<FEduFTransportCharge>
   {
      public FEduFTransportChargeSpecification(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) || x.TransportCharge.ToString().ToLower().Contains(specParams.Search))
      {
         AddInclude(x => x.Transport);
         AddInclude(x => x.TransportArea);
         AddOrderByDescending(s => s.TransportChargeId);
         ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

         if (!string.IsNullOrEmpty(specParams.Sort))
         {
            switch (specParams.Sort)
            {
               case "nameAsc": AddOrderBy(s => s.TransportCharge); break;
               case "nameDesc": AddOrderByDescending(s => s.TransportCharge); break;
               default: AddOrderBy(s => s.TransportChargeId); break;
            }
         }
      }
      public FEduFTransportChargeSpecification(long id) : base(e => e.TransportChargeId == id)
      {
         AddInclude(x => x.Transport);
         AddInclude(x => x.TransportArea);
      }
   }

   public class FEduFTransportChargeCount : BaseSpecification<FEduFTransportCharge>
   {
      public FEduFTransportChargeCount(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) || x.TransportCharge.ToString().ToLower().Contains(specParams.Search))
      {
      }
   }

   public class FEduFTransportChargeDelete : BaseSpecification<FEduFTransportCharge>
   {
      public FEduFTransportChargeDelete(long id) : base(x => x.TransportChargeId == id)
      {
      }
   }
}
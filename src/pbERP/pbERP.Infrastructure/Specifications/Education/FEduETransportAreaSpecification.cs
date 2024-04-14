using pbERP.Domain.Models.FEducation;

namespace pbERP.Infrastructure.Specifications.Education
{
   public class FEduETransportAreaSpecification : BaseSpecification<FEduETransportArea>
   {
      public FEduETransportAreaSpecification(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) || x.TransportAreaName.ToLower().Contains(specParams.Search))
      {
         AddOrderByDescending(s => s.TransportAreaId);
         ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

         if (!string.IsNullOrEmpty(specParams.Sort))
         {
            switch (specParams.Sort)
            {
               case "nameAsc": AddOrderBy(s => s.TransportAreaName); break;
               case "nameDesc": AddOrderByDescending(s => s.TransportAreaName); break;
               default: AddOrderBy(s => s.TransportAreaId); break;
            }
         }
      }
      public FEduETransportAreaSpecification(long id) : base(e => e.TransportAreaId == id)
      {
      }
   }

   public class FEduETransportAreaCount : BaseSpecification<FEduETransportArea>
   {
      public FEduETransportAreaCount(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) || x.TransportAreaName.ToLower().Contains(specParams.Search))
      {
      }
   }

   public class FEduETransportAreaDelete : BaseSpecification<FEduETransportArea>
   {
      public FEduETransportAreaDelete(long id) : base(x => x.TransportAreaId == id)
      {
         AddInclude(x => x.FEduFTransportCharges);
         AddInclude(x => x.FEduGLinkTransportAreas);
         AddInclude(x => x.FEduHStudentAllocateTransports);
      }
   }
}
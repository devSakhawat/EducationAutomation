using pbERP.Domain.Models.FEducation;

namespace pbERP.Infrastructure.Specifications.Education
{
   public class FEduHStudentAllocateTransportSpecification : BaseSpecification<FEduHStudentAllocateTransport>
   {
      public FEduHStudentAllocateTransportSpecification(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) || x.TransportId.ToString().ToLower().Contains(specParams.Search))
      {
         AddInclude(x => x.Transport);
         AddInclude(x => x.TransportArea);
         AddOrderByDescending(s => s.AllocateTransportId);
         ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

         if (!string.IsNullOrEmpty(specParams.Sort))
         {
            switch (specParams.Sort)
            {
               case "nameAsc": AddOrderBy(s => s.StudentId); break;
               case "nameDesc": AddOrderByDescending(s => s.StudentId); break;
               default: AddOrderBy(s => s.AllocateTransportId); break;
            }
         }
      }
      public FEduHStudentAllocateTransportSpecification(long id) : base(e => e.AllocateTransportId == id)
      {
         AddInclude(x => x.Transport);
         AddInclude(x => x.TransportArea);
      }
   }

   public class FEduHStudentAllocateTransportCount : BaseSpecification<FEduHStudentAllocateTransport>
   {
      public FEduHStudentAllocateTransportCount(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) || x.StudentId.ToString().ToLower().Contains(specParams.Search))
      {
      }
   }

   public class FEduHStudentAllocateTransportDelete : BaseSpecification<FEduHStudentAllocateTransport>
   {
      public FEduHStudentAllocateTransportDelete(long id) : base(x => x.AllocateTransportId == id)
      {
      }
   }
}
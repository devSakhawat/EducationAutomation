using pbERP.Domain.Models.FEducation;

namespace pbERP.Infrastructure.Specifications.Education
{
   public class FEduDStudentAllocateHallSeatSpecification : BaseSpecification<FEduDStudentAllocateHallSeat>
   {
      public FEduDStudentAllocateHallSeatSpecification(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) || x.HallSeatId.ToString().ToLower().Contains(specParams.Search))
      {
         AddInclude(x => x.ClassRoom);
         AddInclude(x => x.HallSeat);
         ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
         AddOrderByDescending(s => s.AllocateStudentHallSeatId);

         if (!string.IsNullOrEmpty(specParams.Sort))
         {
            switch (specParams.Sort)
            {
               case "nameAsc": AddOrderBy(s => s.HallSeatId); break;
               case "nameDesc": AddOrderByDescending(s => s.HallSeatId); break;
               default: AddOrderBy(s => s.AllocateStudentHallSeatId); break;
            }
         }
      }
      public FEduDStudentAllocateHallSeatSpecification(long id) : base(e => e.AllocateStudentHallSeatId == id)
      {
         AddInclude(x => x.ClassRoom);
         AddInclude(x => x.HallSeat);
      }
   }

   public class FEduDStudentAllocateHallSeatCount : BaseSpecification<FEduDStudentAllocateHallSeat>
   {
      public FEduDStudentAllocateHallSeatCount(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) || x.HallSeatId.ToString().ToLower().Contains(specParams.Search))
      {
      }
   }

   public class FEduDStudentAllocateHallSeatDelete : BaseSpecification<FEduDStudentAllocateHallSeat>
   {
      public FEduDStudentAllocateHallSeatDelete(long id) : base(x => x.AllocateStudentHallSeatId == id)
      {
      }
   }
}
using pbERP.Domain.Models.FEducation;

namespace pbERP.Infrastructure.Specifications.Education
{
   public class FEduCClassOrHallSpecification : BaseSpecification<FEduCClassOrHall>
   {
      public FEduCClassOrHallSpecification(SpecificationParams specParams)
      : base(x => string.IsNullOrEmpty(specParams.Search) || x.HallSeatNumber.ToString().ToLower().Contains(specParams.Search))
      {
         AddInclude(x => x.ClassRoom);
         AddOrderByDescending(s => s.HallSeatId);
         ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

         if (!string.IsNullOrEmpty(specParams.Sort))
         {
            switch (specParams.Sort)
            {
               case "nameAsc": AddOrderBy(s => s.HallSeatNumber.ToString()); break;
               case "nameDesc": AddOrderByDescending(s => s.HallSeatNumber.ToString()); break;
               default: AddOrderBy(s => s.HallSeatId); break;
            }
         }
      }

      //public FEduCClassOrHallSpecification(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) || x.HallSeatNumber.ToString().ToLower().Contains(specParams.Search))
      //{
      //  AddInclude(x => x.ClassRoom);
      //  AddOrderByDescending(s => s.HallSeatId);
      //  ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

      //  if (!string.IsNullOrEmpty(specParams.Sort))
      //  {
      //    switch (specParams.Sort)
      //    {
      //      case "nameAsc":
      //        AddOrderByDescending(s => s.HallSeatId); break;

      //      case "nameDesc":
      //        AddOrderByDescending(s => s.HallSeatId); break;

      //      default:
      //        AddOrderBy(s => s.ClassRoom.ClassRoomName); break;
      //    }

      //  }
      //}

      //public FEduCClassOrHallSpecification(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search.ToString()) || x.HallSeatNumber.ToString().Contains(specParams.Search))
      //{
      //  AddInclude(x => x.ClassRoom);
      //  AddOrderByDescending(s => s.HallSeatId);
      //  ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

      //  if (!string.IsNullOrEmpty(specParams.Sort))
      //  {
      //    switch (specParams.Sort)
      //    {
      //      case "nameAsc":
      //        AddOrderByDescending(s => s.HallSeatId); break;

      //      case "nameDesc":
      //        AddOrderByDescending(s => s.HallSeatId); break;

      //      default:
      //        AddOrderBy(s => s.HallSeatNumber.ToString()); break;
      //    }
      //  }
      //}
      public FEduCClassOrHallSpecification(long id) : base(e => e.HallSeatId == id)
      {
         AddInclude(x => x.ClassRoom);
      }
   }

   public class FEduCClassOrHallCount : BaseSpecification<FEduCClassOrHall>
   {
      public FEduCClassOrHallCount(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) || x.HallSeatNumber.ToString().ToLower().Contains(specParams.Search))
      {
      }
   }

   public class FEduCClassOrHallDelete : BaseSpecification<FEduCClassOrHall>
   {
      public FEduCClassOrHallDelete(long id) : base(x => x.HallSeatId == id)
      {
         AddInclude(x => x.FEduDStudentAllocateHallSeats);
      }
   }
}
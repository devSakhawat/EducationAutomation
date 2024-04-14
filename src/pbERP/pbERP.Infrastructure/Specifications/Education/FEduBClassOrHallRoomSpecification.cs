using pbERP.Domain.Models.FEducation;

namespace pbERP.Infrastructure.Specifications.Education
{
   public class FEduBClassOrHallRoomSpecification : BaseSpecification<FEduBClassOrHallRoom>
   {
      public FEduBClassOrHallRoomSpecification(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) || x.ClassRoomName.ToLower().Contains(specParams.Search))
      {
         AddInclude(x => x.Building);
         AddOrderByDescending(s => s.ClassRoomId);
         ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

         if (!string.IsNullOrEmpty(specParams.Sort))
         {
            switch (specParams.Sort)
            {
               case "nameAsc": AddOrderBy(s => s.ClassRoomName); break;
               case "nameDesc": AddOrderByDescending(s => s.ClassRoomName); break;
               default: AddOrderBy(s => s.BuildingId); break;
            }
         }
      }
      public FEduBClassOrHallRoomSpecification(long id) : base(e => e.ClassRoomId == id)
      {
         AddInclude(x => x.Building);
      }
   }

   public class FEduBClassOrHallRoomCount : BaseSpecification<FEduBClassOrHallRoom>
   {
      public FEduBClassOrHallRoomCount(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) || x.ClassRoomName.ToLower().Contains(specParams.Search))
      {
      }
   }

   public class FEduBClassOrHallRoomDelete : BaseSpecification<FEduBClassOrHallRoom>
   {
      public FEduBClassOrHallRoomDelete(long id) : base(x => x.ClassRoomId == id)
      {
         AddInclude(x => x.FEduCClassOrHalls);
         AddInclude(x => x.FEduDStudentAllocateHallSeats);
      }
   }
}